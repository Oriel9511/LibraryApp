namespace LibraryApp.Domain.Entities;

public class Genre : BaseAuditableEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
