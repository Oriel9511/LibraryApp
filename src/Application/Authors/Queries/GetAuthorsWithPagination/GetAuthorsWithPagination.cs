using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Mappings;
using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Authors;

namespace LibraryApp.Application.Authors.Queries.GetAuthorsWithPagination;

public record GetAuthorsWithPaginationQuery : IRequest<PaginatedList<AuthorDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetAuthorsWithPaginationQueryHandler : IRequestHandler<GetAuthorsWithPaginationQuery, PaginatedList<AuthorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAuthorsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<AuthorDto>> Handle(GetAuthorsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Authors
            .OrderBy(x => x.Name)
            .ProjectTo<AuthorDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
