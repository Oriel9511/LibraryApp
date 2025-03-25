using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<TodoList> TodoLists { get; }

    DbSet<TodoItem> TodoItems { get; }
    
    DbSet<Author> Authors { get; }
    
    DbSet<Book> Books { get; }
    
    DbSet<Domain.Entities.Genre> Genres { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
