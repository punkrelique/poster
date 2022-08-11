using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Poster.Domain;
using Poster.Infrastructure.EntityTypeConfigurations;

namespace Poster.Infrastructure;

public class ApplicationDbContext : IdentityDbContext, IApplicationDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Message> Messages { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}