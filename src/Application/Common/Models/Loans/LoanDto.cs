using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.Common.Models.Loans;

public class LoanDto
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? BookName { get; set; }
    public string? ISBN { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
}
