using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Mappings;
using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Books;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Books.Queries.GetBooksWithPagination;

public record GetBooksWithPaginationQuery : IRequest<PaginatedList<BookDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetBooksWithPaginationQueryHandler : IRequestHandler<GetBooksWithPaginationQuery, PaginatedList<BookDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetBooksWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BookDto>> Handle(GetBooksWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Include(b => b.Genre)
            .Include(b => b.Author)
            .OrderBy(x => x.Title)
            .AsNoTracking()
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider, new { MaxDepth = 1 })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
