using LibraryApp.Domain.Entities;
using LibraryApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryApp.Infrastructure.Data.Configurations;

public class LoansConfiguration : IEntityTypeConfiguration<Loan>
{
    public void Configure(EntityTypeBuilder<Loan> builder)
    {
        builder.Property(l => l.UserId)
            .IsRequired();

        builder.Property(l => l.LoanDate)
            .IsRequired();

        builder.Property(l => l.ReturnDate)
            .IsRequired();

        builder.HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne<ApplicationUser>()
            .WithMany(u => u.Loans)
            .HasForeignKey(l => l.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
