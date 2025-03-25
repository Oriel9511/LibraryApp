using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.Books.Commands.UpdateBook;

public record UpdateBookCommand : IRequest
{
    public int Id { get; init; }
    public string? Title { get; init; }
    public string? Resume { get; init; }
    public int PublicationYear { get; init; }
    public string? ImageUrl { get; init; }
    public int AuthorId { get; init; }
    public int GenreId { get; init; }
    public string? ISBN { get; init; }
    public int Stock { get; init; }
}

public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IAppLogger _logger;

    public UpdateBookCommandHandler(IApplicationDbContext context, IAppLogger logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Books
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Title = request.Title;
        entity.Resume = request.Resume;
        entity.PublicationYear = request.PublicationYear;
        entity.ImageUrl = request.ImageUrl;
        entity.AuthorId = request.AuthorId;
        entity.GenreId = request.GenreId;
        entity.ISBN = request.ISBN;
        entity.Stock = request.Stock;
        
        await _context.SaveChangesAsync(cancellationToken);
        _logger.LogInformation("Book {ISBN} Updated", entity.ISBN ?? "");
    }
}
