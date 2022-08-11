using Poster.Domain;

namespace Poster.Application.Models.UserDtos;

public class GetUserDto
{
    public string Id { get; }
    public string Email { get; }
    public string Username { get; }
    public DateTime DateCreated { get; }

    public GetUserDto(User user)
    {
        Id = user.Id;
        Email = user.Email;
        Username = user.UserName;
        DateCreated = user.DateCreated;
    }
}