using Poster.Application.Models.MessageDtos;
using Poster.Application.Models.UserDtos;

namespace Poster.Application.Common.Interfaces;

public interface IMessageService
{
    Task<ResultOfT<GetMessagesDtoVm>> GetFollowingUsersMessages(
        string userId,
        int offset,
        int limit,
        CancellationToken cancellationToken);
    
    Task<ResultOfT<GetMessagesDtoVm>> GetUsersMessages(
        string userId,
        int offset,
        int limit,
        CancellationToken cancellationToken);
    
    Task<ResultOfT<GetMessagesDtoVm>> GetUsersMessagesByUsername(
        string username,
        int offset,
        int limit,
        CancellationToken cancellationToken);

    Task<ResultOfT<MessageDto>> PostMessage(
        string body,
        string userId,
        CancellationToken cancellationToken);

    Task<Result> DeleteMessage(
        Guid messageId,
        string userId,
        CancellationToken cancellationToken);
}