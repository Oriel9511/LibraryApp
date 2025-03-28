namespace LibraryApp.Domain.Entities;

public interface IUser
{
    string Id { get; set; }
    bool Active { get; set; }
    ICollection<Loan> Loans { get; set; }
}
