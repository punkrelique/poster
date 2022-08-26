using Poster.Application.Models.UserDtos;

namespace Poster.Application.Common.Interfaces;

public interface IUserService
{
    Task<ResultOfT<GetUserDto>> GetUser(
    string userId,
     CancellationToken cancellationToken);

    Task<ResultOfT<GetUserDto>> GetUserByUsername(
        string username,
        CancellationToken cancellationToken);
    
    Task<ResultOfT<GetUsersDtoVm>> GetUsers(
        string username,
        int offset,
        int limit,
        CancellationToken cancellationToken);

    Task<ResultOfT<int>> GetFollowersCount(
        string userId,
        CancellationToken cancellationToken);

    Task<Result> FollowUser(
        string from,
        string to, 
        CancellationToken cancellationToken);

    Task<Result> UnfollowUser(
        string from,
        string to,
        CancellationToken cancellationToken);

    Task<bool> UserExists(
        string email,
        string username,
        CancellationToken cancellationToken);

    Task<ResultOfT<bool>> IsFollowed(
        string fromId,
        string toUsername,
        CancellationToken cancellationToken);
}