namespace Poster.Api.Models.UserDtos;

public class UserLoginDto
{
    public string Login { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}