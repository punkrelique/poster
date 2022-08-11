using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Poster.Api.Models.UserDtos;
using Poster.Domain;

namespace Poster.Api.Controllers;

public class AuthorizationController : BaseController
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IConfiguration _configuration;

    public AuthorizationController(
        UserManager<User> userManager,
        SignInManager<User> signInManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    
    [HttpPost]
    [Route("Registration")]
    public async Task<IActionResult> Register(
        [FromForm] UserRegisterDto userRegisterDto)
    {
        userRegisterDto.Validate();
        
        var user = new User
        {
            UserName = userRegisterDto.Username,
            Email = userRegisterDto.Email,
            DateCreated = DateTime.Now
        };
        
        var result = await _userManager.CreateAsync(user, userRegisterDto.Password);
        if (!result.Succeeded)
            return BadRequest(new { Error = "Unable to create account" });
        
        return NoContent();
    }
    
    [HttpPost]
    [Route("Login")]
    public async Task<IActionResult> Login([FromForm] UserLoginDto userLoginDto)
    {
        var username = await _userManager.FindByNameAsync(userLoginDto.Login);
        var email = await _userManager.FindByEmailAsync(userLoginDto.Login);

        if (username != null || email != null)
        {
            var signInResult = await _signInManager.PasswordSignInAsync(
                userLoginDto.Login, 
                userLoginDto.Password,
                false,
                false);

            if (signInResult.Succeeded)
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, "some_id"),
                    new Claim("granny", "cookie")
                };

                var secretBytes = Encoding.UTF8.GetBytes(_configuration["Secrets:Secret"]);

                var signingCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes),
                    SecurityAlgorithms.HmacSha256);

                var from = DateTime.Now;
                var till = DateTime.Now.AddDays(7);
                
                var token = new JwtSecurityToken(
                    _configuration["Secrets:Issuer"],
                    _configuration["Secrets:Audience"],
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
}