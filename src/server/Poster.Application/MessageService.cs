using Microsoft.EntityFrameworkCore;
using Poster.Application.Common;
using Poster.Application.Common.Interfaces;
using Poster.Application.Models.MessageDtos;
using Poster.Domain;
using Poster.Infrastructure;

namespace Poster.Application;

public class MessageService : IMessageService
{
    private readonly IApplicationDbContext _context;

    public MessageService(IApplicationDbContext context)
        => _context = context;
    
    public async Task<ResultOfT<GetMessagesDtoVm>> GetFollowingUsersMessages(
        string userId,
        int offset,
        int limit,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Following)
            .ThenInclude(s => s.Messages.Skip(offset).Take(limit))
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    
        if (user == null)
            return Result.Fail<GetMessagesDtoVm>($"{nameof(User)} with {userId} not found");
        
        return Result.Ok(new GetMessagesDtoVm
        {
            Messages = user.Following
                .SelectMany(users => users.Messages)
                .Select(message => new MessageDto (message.User, message))
                .ToList()
        });
    }

    public async Task<ResultOfT<GetMessagesDtoVm>> GetUsersMessages(
        string userId,
        int offset,
        int limit,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(m => m.Messages)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    
        if (user == null)
            return Result.Fail<GetMessagesDtoVm>($"{nameof(User)} with {userId} not found");
        
        return Result.Ok(new GetMessagesDtoVm
        {
            Messages = user.Messages
                .Select(message => new MessageDto(message.User, message))
                .ToList()
        });
    }

    public async Task<ResultOfT<GetMessagesDtoVm>> GetUsersMessagesByUsername(
        string username,
        int offset,
        int limit,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(m => m.Messages)
            .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);
    
        if (user == null)
            return Result.Fail<GetMessagesDtoVm>($"{nameof(User)} with {username} not found");
        
        return Result.Ok(new GetMessagesDtoVm
        {
            Messages = user.Messages
                .Select(message => new MessageDto(message.User, message))
                .ToList()
        });
    }

    public async Task<ResultOfT<MessageDto>> PostMessage(
        string body,
        string userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
            return Result.Fail<MessageDto>($"User with {userId} not found");

        var message = new Message
        {
            Body = body,
            DateCreated = DateTime.Now,
            UserId = userId,
        };
        
        await _context.Messages.AddAsync(message, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok<MessageDto>(new MessageDto
        {
            Id = message.Id,
            Body = message.Body,
            DateCreated = message.DateCreated,
            Username = message.User.UserName
        });
    }

    public async Task<Result> DeleteMessage(
        Guid messageId,
        string userId,
        CancellationToken cancellationToken)
    {
        var message = await _context.Messages
            .FirstOrDefaultAsync(m => m.Id == messageId, cancellationToken);

        if (message == null)
            return Result.Fail($"{nameof(User)} with {messageId} not found");

        if (message.UserId != userId)
            return Result.Fail("Only author can delete their messages");
        
        _context.Messages.Remove(message);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}