using LibraryApp.Application.Common.Models.Books;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Authors;

public class AuthorDto : BaseDto<Author, AuthorDto>
{
    public string? Name { get; set; }
    public string? Nationality { get; set; }
    public DateTime Birthday { get; set; }
    public virtual ICollection<BookDto> Books { get; set; } = new List<BookDto>();
}
