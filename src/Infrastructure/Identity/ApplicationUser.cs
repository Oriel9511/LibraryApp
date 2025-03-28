using LibraryApp.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Infrastructure.Identity;

public class ApplicationUser : IdentityUser, IUser
{
    public bool Active { get; set; } = true;
    public ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
