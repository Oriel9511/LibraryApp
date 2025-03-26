using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Mappings;
using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Books;

namespace LibraryApp.Application.Books.Queries.GetBooksWithPagination;

public class GetFilteredBooksQuery : IRequest<PaginatedList<BookBasicDto>>
{
    public string? Filter { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetFilteredBooksQueryHandler : IRequestHandler<GetFilteredBooksQuery, PaginatedList<BookBasicDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetFilteredBooksQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<BookBasicDto>> Handle(GetFilteredBooksQuery request, CancellationToken cancellationToken)
    {
        return await _context.Books
            .Include(b => b.Genre)
            .Include(b => b.Author)
            .OrderBy(x => x.Title)
            .AsNoTracking()
            .ProjectTo<BookBasicDto>(_mapper.ConfigurationProvider, new { MaxDepth = 1 })
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
