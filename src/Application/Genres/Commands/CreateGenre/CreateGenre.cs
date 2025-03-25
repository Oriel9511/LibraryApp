using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Entities;
using LibraryApp.Domain.Events;

namespace LibraryApp.Application.Genres.Commands.CreateGenre;

public record CreateGenreCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}

public class CreateGenreCommandHandler : IRequestHandler<CreateGenreCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateGenreCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var entity = new Genre()
        {
            Name = request.Name,
            Description = request.Description,
        };

        _context.Genres.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
