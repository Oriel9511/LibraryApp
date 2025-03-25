namespace LibraryApp.Application.Common.Models;

public class BaseDto<T, TDto>
{
    public int Id { get; set; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<T, TDto>();
        }
    }
}
