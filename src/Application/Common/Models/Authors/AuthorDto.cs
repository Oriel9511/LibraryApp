using LibraryApp.Application.Common.Models.Books;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Authors;

public class AuthorDto : BaseDto
{
    public string? Name { get; set; }
    public string? Nationality { get; set; }
    public DateTime Birthday { get; set; }
    public virtual ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Author, AuthorDto>();
        }
    }
}
