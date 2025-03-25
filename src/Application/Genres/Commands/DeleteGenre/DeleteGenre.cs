using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.Genres.Commands.DeleteGenre;

public record DeleteGenreCommand(int Id) : IRequest;

public class DeleteGenreCommandHandler : IRequestHandler<DeleteGenreCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteGenreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Genres
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Genres.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
