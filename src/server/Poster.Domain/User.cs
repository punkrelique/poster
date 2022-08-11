using Microsoft.AspNetCore.Identity;

namespace Poster.Domain;

public class User : IdentityUser
{
    public DateTime DateCreated { get; set; }
    
    public ICollection<User> Followers { get; set; }
    public ICollection<User> Following { get; set; }
    public ICollection<Message> Messages { get; set; }
}