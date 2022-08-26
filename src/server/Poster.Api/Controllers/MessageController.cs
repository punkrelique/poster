using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Poster.Application.Common.Interfaces;

namespace Poster.Api.Controllers;

[Authorize(AuthenticationSchemes=JwtBearerDefaults.AuthenticationScheme)]
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
        if (offset < 0 || limit < 0)
            return BadRequest(new { Error = "Offset or limit cannot be less than 0" });
        
        var result = await _messageService.GetFollowingUsersMessages(
            UserId,
            offset,
            limit,
            cancellationToken);

        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }
    
    [HttpGet]
    [Route("Caller")]
    public async Task<IActionResult> GetUsersMessages(
        [FromQuery] int offset,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        if (offset < 0 || limit < 0)
            return BadRequest(new { Error = "Offset or limit cannot be less than 0" });
        
        var result = await _messageService.GetUsersMessages(
            UserId,
            offset,
            limit,
            cancellationToken);

        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }
    
    [HttpGet]
    [Route("{username}")]
    public async Task<IActionResult> GetUsersMessagesByUsername(
        string username,
        [FromQuery] int offset,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        if (offset < 0 || limit < 0)
            return BadRequest(new { Error = "Offset or limit cannot be less than 0" });
        
        var result = await _messageService.GetUsersMessagesByUsername(
            username,
            offset,
            limit,
            cancellationToken);

        if (!result.Success)
            return NotFound(new { Error = result.Error });

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

        return Ok(result.Value);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteMessage(
        [FromQuery] Guid messageId,
        CancellationToken cancellationToken)
    {
        var result = await _messageService.DeleteMessage(messageId, UserId, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });
        
        return NoContent();
    }
}