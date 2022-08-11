using Microsoft.EntityFrameworkCore;
using Poster.Domain;

namespace Poster.Infrastructure;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Message> Messages { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}