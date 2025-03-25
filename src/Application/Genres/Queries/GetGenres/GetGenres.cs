using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Models.Genres;

namespace LibraryApp.Application.Genres.Queries.GetGenres;

public record GetGenresQuery : IRequest<List<GenreDto>> { }

public class GetGenresQueryHandler : IRequestHandler<GetGenresQuery, List<GenreDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetGenresQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<GenreDto>> Handle(GetGenresQuery request, CancellationToken cancellationToken)
    {
        return await _context.Genres
            .OrderBy(x => x.Name)
            .ProjectTo<GenreDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
