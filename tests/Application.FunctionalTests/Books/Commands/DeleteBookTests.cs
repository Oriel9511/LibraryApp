using LibraryApp.Application.Authors.Commands.CreateAuthor;
using LibraryApp.Application.Books.Commands.CreateBook;
using LibraryApp.Application.Books.Commands.DeleteBook;
using LibraryApp.Application.Genres.Commands.CreateGenre;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.FunctionalTests.Books.Commands;

using static Testing;

public class DeleteBookTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidBookId()
    {
        var command = new DeleteBookCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteBook()
    {
        var userId = await RunAsDefaultUserAsync();

        var authorId = await SendAsync(new CreateAuthorCommand()
        {
            Name = "New Author",
            Nationality = "New Nationality",
            Birthday = new DateTime(1980, 1, 1),
        });

        var genreId = await SendAsync(new CreateGenreCommand() { Name = "New Genre", Description = "New Description" });

        var command = new CreateBookCommand()
        {
            Title = "New Book",
            AuthorId = authorId,
            GenreId = genreId,
            Stock = 3,
            PublicationYear = 2018,
            ImageUrl = "New Image",
            ISBN = "NewISBN",
            Resume = "New Resume",
        };

        var itemId = await SendAsync(command);

        await SendAsync(new DeleteBookCommand(itemId));

        var item = await FindAsync<Book>(itemId);

        item.Should().BeNull();
    }
}
