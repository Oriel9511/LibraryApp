using LibraryApp.Domain.Entities;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace LibraryApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Author> Authors { get; }
    
    DbSet<Book> Books { get; }
    
    DbSet<Domain.Entities.Genre> Genres { get; }
    
    DbSet<Loan> Loans { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    
    DatabaseFacade Database { get; }
}
