using System.Text.RegularExpressions;
using Poster.Api.Utils;

namespace Poster.Api.Models.UserDtos;

public class UserRegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }

    public void Validate()
    {
        if (Username.Length > 256)
            throw new ArgumentException($"{nameof(Username)} cannot be more than 256 symbols long");
        if (Email.Length > 256)
            throw new ArgumentException($"{nameof(Username)} cannot be more than 256 symbols long");
        if (!RegexIdentifiers.MailRegex.IsMatch(Email))
            throw new ArgumentException($"{nameof(Email)} is invalid");
    }
}