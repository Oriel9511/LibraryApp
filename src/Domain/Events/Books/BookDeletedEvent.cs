namespace LibraryApp.Domain.Events.Books;

public class BookDeletedEvent : BaseEvent
{
    public BookDeletedEvent(Book item)
    {
        Item = item;
    }

    public Book Item { get; }
}
