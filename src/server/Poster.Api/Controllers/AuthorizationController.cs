using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Poster.Api.Models.UserDtos;
using Poster.Application.Common.Interfaces;
using Poster.Domain;

namespace Poster.Api.Controllers;

public class AuthorizationController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthorizationController> _logger;
    private readonly IUserService _userService;

    public AuthorizationController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration,
        ILogger<AuthorizationController> logger,
        IUserService userService)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
        _logger = logger;
        _userService = userService;
    }
    
    [HttpPost]
    [Route("Registration")]
    public async Task<IActionResult> Register(
        [FromForm] UserRegisterDto userRegisterDto,
        CancellationToken cancellationToken)
    {
        var validationResult = userRegisterDto.Validate();
        if (!validationResult.Success)
            return BadRequest(new { Error = validationResult.Error });

        var userExists = await _userService.UserExists(
            userRegisterDto.Email,
            userRegisterDto.Username,
            cancellationToken
        );
        
        if (userExists)
            return BadRequest(new {Error = "User with this username/email already exists"});
        
        var user = new User
        {
            UserName = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            DateCreated = DateTime.Now
        };

        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        if (!result.Succeeded)
            return BadRequest(new { Error = "User with such email already exists" });
        
        return NoContent();
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login(
        [FromForm] UserLoginDto userLoginDto,
        CancellationToken cancellationToken)
    {
        var username = await _userManager.FindByNameAsync(userLoginDto.Login);
        var email = await _userManager.FindByEmailAsync(userLoginDto.Login);

        if (username != null || email != null)
        {
            var user = email ?? username; 
            var signInResult = await _signInManager.PasswordSignInAsync(
                userLoginDto.Login, 
                userLoginDto.Password,
                false,
                false);

            if (signInResult.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                    new Claim(JwtRegisteredClaimNames.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                };

                var secretBytes = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256);

                var from = DateTime.Now;
                var till = DateTime.Now.AddDays(7);
                
                var token = new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    from,
                    till,
                    signingCredentials);

                var tokenJson = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    access_token = tokenJson,
                    starts_at = from,
                    expires = till
                });
            }
        }

        return BadRequest(new { Error = "Unable to login"});
    }

    [HttpPost]
    [Route("Refresh")]
    public async Task RefreshToken(CancellationToken cancellationToken)
    {
        
    }
}