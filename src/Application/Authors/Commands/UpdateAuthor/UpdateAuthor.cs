using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.Authors.Commands.UpdateAuthor;

public record UpdateAuthorCommand : IRequest
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Nationality { get; init; }
    public DateTime Birthday { get; init; }
}

public class UpdateAuthorCommandHandler : IRequestHandler<UpdateAuthorCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateAuthorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Authors
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        entity.Name = request.Name;
        entity.Nationality = request.Nationality;
        entity.Birthday = request.Birthday;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
