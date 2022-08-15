﻿using Microsoft.EntityFrameworkCore;
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
    
        var messages = new List<MessageDto>();
        foreach (var u in user.Following)
        {
            foreach (var message in u.Messages)
                messages.Add(new MessageDto(u, message));
        }
        
        return Result.Ok(new GetMessagesDtoVm { Messages = messages });
    }

    public async Task<Result> PostMessage(
        string body,
        string userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
        if (user == null)
            return Result.Fail($"User with {userId} not found");
            
        await _context.Messages.AddAsync(new Message
        {
            Body = body,
            DateCreated = DateTime.Now,
            UserId = userId,
        }, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
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
            return Result.Fail("Only author can delete his messages");
        
        _context.Messages.Remove(message);
        
        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}