using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.Genres.Commands.UpdateGenre;

public record UpdateGenreCommand : IRequest
{
    public int Id { get; init; }

    public string? Name { get; init; }

    public string? Description { get; init; }
}

public class UpdateGenreCommandHandler : IRequestHandler<UpdateGenreCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateGenreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Genres
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
