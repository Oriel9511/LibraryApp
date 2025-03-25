using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.Authors.Commands.DeleteAuthor;

public record DeleteAuthorCommand(int Id) : IRequest;

public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteAuthorCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Authors
            .FindAsync(new object[] { request.Id }, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        _context.Authors.Remove(entity);

        await _context.SaveChangesAsync(cancellationToken);
    }

}
