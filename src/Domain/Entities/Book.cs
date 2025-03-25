namespace LibraryApp.Domain.Entities;

public class Book : BaseAuditableEntity
{
    public string? Title { get; set; }
    public string? Resume { get; set; }
    public int PublicationYear { get; set; }
    public string? ImageUrl { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public string? ISBN { get; set; }
    public int Stock { get; set; }
    public virtual Author Author { get; set; } = new Author();
    public virtual Genre Genre { get; set; } = new Genre();
}
