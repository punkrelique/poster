namespace Poster.Domain;

public class Message
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; }
    
    public string UserId { get; set; }
    public User User { get; set; }
}