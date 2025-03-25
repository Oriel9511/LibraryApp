using LibraryApp.Application.Common.Models.Authors;
using LibraryApp.Application.Common.Models.Genres;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Books;

public class BookDto : BaseDto
{
    public string? Title { get; set; }
    public string? Resume { get; set; }
    public int PublicationYear { get; set; }
    public string? ImageUrl { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public string? ISBN { get; set; }
    public int Stock { get; set; }
    public virtual AuthorDto Author { get; set; } = new AuthorDto();
    public virtual GenreDto Genre { get; set; } = new GenreDto();
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookDto>();
        }
    }
}


