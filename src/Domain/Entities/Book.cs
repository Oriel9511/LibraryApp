﻿namespace LibraryApp.Domain.Entities;

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
    public Author? Author { get; set; }
    public Genre? Genre { get; set; }
    
    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
