using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Data.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(t => t.Title)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.Resume)
            .HasMaxLength(2000)
            .IsRequired();

        builder.Property(t => t.PublicationYear)
            .IsRequired();

        builder.Property(t => t.ImageUrl)
            .HasMaxLength(500)
            .IsRequired();

        builder.Property(t => t.AuthorId)
            .IsRequired();

        builder.Property(t => t.GenreId)
            .IsRequired();

        builder.Property(t => t.ISBN)
            .HasMaxLength(13)
            .IsRequired();

        builder.Property(t => t.Stock)
            .IsRequired();

        // builder.HasOne(b => b.Author)
        //     .WithMany()
        //     .HasForeignKey(b => b.AuthorId)
        //     .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(b => b.Genre)
            .WithMany()
            .HasForeignKey(b => b.GenreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
