namespace LibraryApp.Domain.Events.Genres;

public class GenreCreateEvent : BaseEvent
{
    public GenreCreateEvent(Genre item)
    {
        Item = item;
    }

    public Genre Item { get; }
}
