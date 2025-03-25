using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Events;

namespace LibraryApp.Application.Books.Commands.DeleteBook;

public record DeleteBookCommand(int Id) : IRequest;

public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteBookCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Books
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Books.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
