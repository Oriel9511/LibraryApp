using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Events;

namespace LibraryApp.Application.Books.Commands.CreateBook;

public record CreateBookCommand : IRequest<int>
{
    public string? Title { get; init; }
    public string? Resume { get; init; }
    public int PublicationYear { get; init; }
    public string? ImageUrl { get; init; }
    public int AuthorId { get; init; }
    public int GenreId { get; init; }
    public string? ISBN { get; init; }
    public int Stock { get; init; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IAppLogger _logger;

    public CreateBookCommandHandler(IApplicationDbContext context, IAppLogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = new Book()
        {
            Title = request.Title,
            PublicationYear = request.PublicationYear,
            Resume = request.Resume,
            ImageUrl = request.ImageUrl,
            AuthorId = request.AuthorId,
            GenreId = request.GenreId,
            ISBN = request.ISBN,
            Stock = request.Stock,
        };

        _context.Books.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);
        
        _logger.LogInformation("Book {ISBN} Created", entity.ISBN ?? "");

        return entity.Id;
    }
}
