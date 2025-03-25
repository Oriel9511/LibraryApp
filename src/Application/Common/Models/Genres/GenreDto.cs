using LibraryApp.Application.Common.Models.Books;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Genres;

public class GenreDto : BaseDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Genre, GenreDto>();
        }
    }
}
