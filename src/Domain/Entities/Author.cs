namespace LibraryApp.Domain.Entities;

public class Author : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Nationality { get; set; }
    public DateTime Birthday { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
