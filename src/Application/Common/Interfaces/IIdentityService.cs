using LibraryApp.Application.Common.Models;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<string?> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);
    
    IQueryable<UserDto> GetUsersAsQueryable();
    
    Task<string> GetPassword(string email);
}
