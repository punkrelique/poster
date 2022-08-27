using Microsoft.EntityFrameworkCore;
using Poster.Application.Common;
using Poster.Application.Common.Interfaces;
using Poster.Application.Models.UserDtos;
using Poster.Domain;
using Poster.Infrastructure;

namespace Poster.Application;

public class UserService : IUserService
{
    private readonly IApplicationDbContext _context;

    public UserService(IApplicationDbContext context)
        =>_context = context;
    
    public async Task<ResultOfT<GetUserDto>> GetUser(
        string userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
            return Result.Fail<GetUserDto>($"{nameof(User)} with {userId} not found");

        return Result.Ok(new GetUserDto(user));
    }

    public async Task<ResultOfT<GetUserDto>> GetUserByUsername(
        string username,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.UserName == username, cancellationToken);

        if (user == null)
            return Result.Fail<GetUserDto>($"{nameof(User)} with {username} not found");

        return Result.Ok(new GetUserDto(user));
    }

    public async Task<ResultOfT<GetUsersDtoVm>> GetUsers(
        string username,
        int offset,
        int limit,
        CancellationToken cancellationToken)
    {
        var users = await _context.Users
            .AsNoTracking()
            .Where(u => u.UserName.StartsWith(username))
            .Skip(offset)
            .Take(limit)
            .ToListAsync(cancellationToken);

        return Result.Ok(new GetUsersDtoVm(users));
    }
    
    public async Task<ResultOfT<int>> GetFollowersCount(
        string userId,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .Include(u => u.Followers)
            .FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);

        if (user == null)
            return Result.Fail<int>($"{nameof(User)} with {userId} not found");

        return Result.Ok(user.Followers.Count);
    }

    public async Task<Result> FollowUser(
        string from,
        string to, 
        CancellationToken cancellationToken)
    {
        var fromUser = await _context.Users
            .Include(i => i.Following)
            .FirstOrDefaultAsync(
            u => u.Id == from, cancellationToken);

        if (fromUser == null)
            return Result.Fail<GetUserDto>($"{nameof(User)} with {from} not found");

        var toUser = await _context.Users
            .Include(i => i.Followers)
            .FirstOrDefaultAsync(
            u => u.Id == to, cancellationToken);

        if (toUser == null)
            return Result.Fail<GetUserDto>($"{nameof(User)} with {to} not found");

        if (fromUser.Following.Any(u => u.Id == to))
            return Result.Fail<GetUserDto>($"{nameof(User)} with {from} is already following {to}");
        
        fromUser.Following.Add(toUser);
        toUser.Followers.Add(fromUser);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<Result> UnfollowUser(
        string from, 
        string to, 
        CancellationToken cancellationToken)
    {
        var fromUser = await _context.Users
            .Include(i => i.Following)
            .FirstOrDefaultAsync(
                u => u.Id == from, cancellationToken);

        if (fromUser == null)
            return Result.Fail<GetUserDto>($"{nameof(User)} with {from} not found");

        var toUser = await _context.Users
            .Include(i => i.Followers)
            .FirstOrDefaultAsync(
                u => u.Id == to, cancellationToken);

        if (toUser == null)
            return Result.Fail<GetUserDto>($"{nameof(User)} with {to} not found");

        if (!fromUser.Following.Any(u => u.Id == to))
            return Result.Fail<GetUserDto>($"{nameof(User)} with {from} is not following {to}");

        fromUser.Following.Remove(toUser);
        toUser.Followers.Remove(fromUser);

        await _context.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }

    public async Task<bool> UserExists(
        string email,
        string username,
        CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email || u.UserName == username, cancellationToken);

        return user != null;
    }

    public async Task<ResultOfT<bool>> IsFollowed(
        string fromId,
        string toUsername,
        CancellationToken cancellationToken)
    {
        var userFrom = await _context.Users
            .AsNoTracking()
            .Include(i => i.Following)
            .FirstOrDefaultAsync(user => user.Id == fromId, cancellationToken);

        if (userFrom == null)
            return Result.Fail<bool>($"{nameof(User)} with {fromId} not found");

        return userFrom.Following.FirstOrDefault(user => user.UserName == toUsername) == null
            ? Result.Fail<bool>("")
            : Result.Ok<bool>(true);
    }
}