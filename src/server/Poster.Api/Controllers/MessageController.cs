using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poster.Application.Common.Interfaces;

namespace Poster.Api.Controllers;

[Authorize]
public class MessageController : BaseController
{
    private readonly IMessageService _messageService;

    public MessageController(IMessageService messageService) 
        => _messageService = messageService;
    
    [HttpGet]
    public async Task<IActionResult> GetFollowingUsersMessages(
        [FromQuery] int offset,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var result = await _messageService.GetFollowingUsersMessages(
            UserId,
            offset,
            limit,
            cancellationToken);

        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost]
    public async Task<IActionResult> PostMessage(
        [FromForm] string body,
        CancellationToken cancellationToken)
    {
        var result = await _messageService.PostMessage(body, UserId, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMessage(
        [FromForm] Guid messageId,
        CancellationToken cancellationToken)
    {
        var result = await _messageService.DeleteMessage(messageId, UserId, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });
        
        return NoContent();
    }
}