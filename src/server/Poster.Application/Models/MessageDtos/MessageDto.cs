using Poster.Application.Common.Interfaces;
using Poster.Domain;

namespace Poster.Application.Models.MessageDtos;

public class MessageDto
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; }
    public string Username { get; set; }

    public MessageDto(User user, Message message)
    {
        Id = message.Id;
        Body = message.Body;
        DateCreated = message.DateCreated;
        Username = user.UserName;
    }

    public MessageDto()
    {
        
    }
}