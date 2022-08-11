using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poster.Domain;

namespace Poster.Infrastructure.EntityTypeConfigurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasMany(e => e.Messages).WithOne(e => e.User);
        builder.HasMany(e => e.Followers).WithMany(e => e.Following);
        builder.Property(e => e.DateCreated).HasColumnType("date");
    }
}