using Poster.Application.Models.MessageDtos;
using Poster.Application.Models.UserDtos;

namespace Poster.Application.Common.Interfaces;

public interface IUserService
{
    Task<ResultOfT<GetUserDto>> GetUser(string userId, CancellationToken cts);
    
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
}