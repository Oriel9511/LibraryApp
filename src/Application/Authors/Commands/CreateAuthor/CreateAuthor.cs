using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand : IRequest<int>
{
    public string? Name { get; init; }
    public string? Nationality { get; init; }
    public DateTime Birthday { get; init; }
}

public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAuthorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = new Author()
        {
            Name = request.Name,
            Nationality = request.Nationality,
            Birthday = request.Birthday,
        };

        _context.Authors.Add(entity);
            
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
