using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace Poster.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class BaseController : ControllerBase
{
    internal string UserId => !User.Identity.IsAuthenticated
        ? string.Empty
        : (User.FindFirst(ClaimTypes.NameIdentifier).Value);
}