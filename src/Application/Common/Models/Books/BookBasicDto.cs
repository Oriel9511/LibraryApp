using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Books;

public class BookBasicDto : BaseDto
{
    public string? Title { get; set; }
    public string? Resume { get; set; }
    public int PublicationYear { get; set; }
    public string? ImageUrl { get; set; }
    public string? ISBN { get; set; }
    public int Stock { get; set; }
    public string? Author { get; set; }
    public string? Genre { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Book, BookBasicDto>()
                .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author != null ? src.Author.Name : null))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre != null ? src.Genre.Name : null));
        }
    }
}
