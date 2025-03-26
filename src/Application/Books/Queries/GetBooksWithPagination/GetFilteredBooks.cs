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
        var query = _context.Books
            .Include(b => b.Genre)
            .Include(b => b.Author)
            .AsNoTracking();

        if (!string.IsNullOrEmpty(request.Filter))
        {
            query = query.Where(b => 
                b.Title!.Contains(request.Filter) ||
                b.ISBN!.Contains(request.Filter) ||
                b.Author!.Name!.Contains(request.Filter) ||
                b.Genre!.Name!.Contains(request.Filter));
        }

        return await query
            .OrderBy(x => x.Title)
            .ProjectTo<BookBasicDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);    
    }
}
