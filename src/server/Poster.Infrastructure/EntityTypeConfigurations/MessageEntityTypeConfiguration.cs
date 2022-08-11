using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Poster.Domain;

namespace Poster.Infrastructure.EntityTypeConfigurations;

public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).ValueGeneratedOnAdd();
        builder.Property(e => e.Body).IsRequired();
        builder.Property(e => e.DateCreated).IsRequired();
        builder.HasOne(e => e.User);
        builder.Property(e => e.DateCreated).HasColumnType("date");
    }
}