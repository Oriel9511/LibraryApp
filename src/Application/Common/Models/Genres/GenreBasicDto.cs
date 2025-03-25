using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Genres;

public class GenreBasicDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Genre, GenreBasicDto>();
        }
    }
}
