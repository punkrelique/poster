using Microsoft.AspNetCore.Mvc;
using Poster.Application.Common.Interfaces;

namespace Poster.Api.Controllers;

public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
        => _userService = userService;

    [HttpGet]
    public async Task<IActionResult> GetUser(
        string userId,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetUser(userId, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("List")]
    public async Task<IActionResult> GetUsers(
        string username,
        [FromQuery] int offset,
        [FromQuery] int limit,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetUsers(username, offset, limit, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }

    [HttpGet]
    [Route("Followers/{userId}")]
    public async Task<IActionResult> GetFollowersCount(
        string userId,
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetFollowersCount(userId, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }
    
    [HttpGet]
    [Route("Followers")]
    public async Task<IActionResult> GetFollowersCount(
        CancellationToken cancellationToken)
    {
        var result = await _userService.GetFollowersCount(UserId, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return Ok(result.Value);
    }

    [HttpPost("Follow")]
    public async Task<IActionResult> FollowUser(
        [FromQuery] string to,
        CancellationToken cancellationToken)
    {
        if (UserId == to)
            return BadRequest(new { Error = "Cannot unfollow yourself" });
                
        var result = await _userService.FollowUser(UserId, to, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });
        
        return NoContent();
    } 
    
    [HttpPost("Unfollow")]
    public async Task<IActionResult> UnfollowUser(
        [FromQuery] string to,
        CancellationToken cancellationToken)
    {
        if (UserId == to)
            return BadRequest(new { Error = "Cannot follow yourself" });
        
        var result = await _userService.UnfollowUser(UserId, to, cancellationToken);
        if (!result.Success)
            return BadRequest(new { Error = result.Error });

        return NoContent();
    } 
}