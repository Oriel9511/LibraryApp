namespace LibraryApp.Domain.Entities;

public class Loan : BaseAuditableEntity
{ 
    public string? UserId { get; set; }
    public int BookId { get; set; }
    public Book? Book { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime? ReturnDate { get; set; }
}

