using LibraryApp.Application.Authors.Commands.CreateAuthor;
using LibraryApp.Application.Books.Commands.CreateBook;
using LibraryApp.Application.Common.Exceptions;
using LibraryApp.Application.Genres.Commands.CreateGenre;
using LibraryApp.Domain.Entities;

namespace LibraryApp.Application.FunctionalTests.Books.Commands;

using static Testing;

public class CreateBookTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateBookCommand();

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldCreateBook()
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

        var item = await FindAsync<Book>(itemId);

        item.Should().NotBeNull();
        item!.AuthorId.Should().Be(command.AuthorId);
        item.Title.Should().Be(command.Title);
        item.CreatedBy.Should().Be(userId);
        item.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
        item.LastModifiedBy.Should().Be(userId);
        item.LastModified.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
