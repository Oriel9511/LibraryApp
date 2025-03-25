using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Authors;

public class AuthorBasicDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Nationality { get; set; }
    public DateTime Birthday { get; set; }
    public int TotalPublishedBooks { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Author, AuthorBasicDto>()
                .ForMember(dest => dest.TotalPublishedBooks, 
                    opt => opt.MapFrom(src => src.Books.Count));
        }
    }
}
