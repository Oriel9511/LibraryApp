namespace LibraryApp.Domain.Events.Books;

public class BookCreatedEvent : BaseEvent
{
    public BookCreatedEvent(Book item)
    {
        Item = item;
    }

    public Book Item { get; }
}
