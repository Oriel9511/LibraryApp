using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Data.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasMany(t => t.Books)
            .WithOne(b => b.Genre)
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
