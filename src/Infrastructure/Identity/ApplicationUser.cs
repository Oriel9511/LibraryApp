using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    bool Active { get; set; }
}
