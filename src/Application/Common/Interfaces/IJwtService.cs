namespace LibraryApp.Application.Common.Interfaces;

public interface IJwtService
{
    Task<string> CreateToken(string userName, string password);
}
