using System.Text.RegularExpressions;
using Poster.Api.Utils;
using Poster.Application.Common;

namespace Poster.Api.Models.UserDtos;

public class UserRegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Result Validate()
    {
        if (Username.Length > 256)
            return Result.Fail($"{nameof(Username)} cannot be more than 256 symbols long");
        if (Username.Length < 2)
            return Result.Fail($"{nameof(Username)} cannot be more less than 2 symbols long");
        if (!RegexIdentifiers.UsernameRegex.IsMatch(Username))
            return Result.Fail($"{nameof(Username)} can only contain letters, digits and underscore");
        if (Email.Length > 256)
            return Result.Fail($"{nameof(Username)} cannot be more than 256 symbols long");
        if (!RegexIdentifiers.MailRegex.IsMatch(Email))
            return Result.Fail($"{nameof(Email)} is invalid");
        
        return Result.Ok();
    }
}