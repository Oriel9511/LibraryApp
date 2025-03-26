using LibraryApp.Application.Authors.Commands.CreateAuthor;
using LibraryApp.Application.Books.Commands.CreateBook;
using LibraryApp.Application.Books.Queries.GetBooksWithPagination;
using LibraryApp.Application.Genres.Commands.CreateGenre;

namespace LibraryApp.Application.FunctionalTests.Books.Queries;

using static Testing;

public class GetFilteredBooksTests : BaseTestFixture
{
    [Test]
    public async Task ShouldReturnFilteredBooks()
    {
        await RunAsDefaultUserAsync();

        var programmingGenreId = await SendAsync(new CreateGenreCommand 
        { 
            Name = "Programming", 
            Description = "Programming books" 
        });

        var robertMartinId = await SendAsync(new CreateAuthorCommand
        {
            Name = "Robert C. Martin",
            Nationality = "American",
            Birthday = new DateTime(1952, 12, 5)
        });

        var erichGammaId = await SendAsync(new CreateAuthorCommand
        {
            Name = "Erich Gamma",
            Nationality = "Swiss",
            Birthday = new DateTime(1961, 3, 13)
        });
        
        await SendAsync(new CreateBookCommand
        {
            Title = "Clean Code",
            Resume = "A Handbook of Agile Software Craftsmanship",
            Stock = 5,
            PublicationYear = 2008,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41jEbK-jG+L._SX374_BO1,204,203,200_.jpg",
            ISBN = "978-013235",
            AuthorId = robertMartinId,
            GenreId = programmingGenreId
        });

        await SendAsync(new CreateBookCommand
        {
            Title = "Design Patterns",
            Resume = "Elements of Reusable Object-Oriented Software",
            Stock = 3,
            PublicationYear = 1994,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41+1B4y6qPL._SX381_BO1,204,203,200_.jpg",
            ISBN = "978-020163",
            AuthorId = erichGammaId,
            GenreId = programmingGenreId
        });        
        
        var query = new GetFilteredBooksQuery
        {
            Filter = "Clean",
            PageNumber = 1,
            PageSize = 10
        };

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(1);
        result.Items.First().Title.Should().Be("Clean Code");
        result.TotalCount.Should().Be(1);
    }

    [Test]
    public async Task ShouldReturnAllBooksWhenNoFilter()
    {
        await RunAsDefaultUserAsync();

        var programmingGenreId = await SendAsync(new CreateGenreCommand 
        { 
            Name = "Programming",
            Description = "Programming books"
        });

        var robertMartinId = await SendAsync(new CreateAuthorCommand
        {
            Name = "Robert C. Martin",
            Nationality = "American",
            Birthday = new DateTime(1952, 12, 5)
        });

        var erichGammaId = await SendAsync(new CreateAuthorCommand
        {
            Name = "Erich Gamma",
            Nationality = "Swiss",
            Birthday = new DateTime(1961, 3, 13)
        });

        await SendAsync(new CreateBookCommand
        {
            Title = "Clean Code",
            Resume = "A Handbook of Agile Software Craftsmanship",
            Stock = 5,
            PublicationYear = 2008,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41jEbK-jG+L._SX374_BO1,204,203,200_.jpg",
            ISBN = "978-013235",
            AuthorId = robertMartinId,
            GenreId = programmingGenreId
        });

        await SendAsync(new CreateBookCommand
        {
            Title = "Design Patterns",
            Resume = "Elements of Reusable Object-Oriented Software",
            Stock = 3,
            PublicationYear = 1994,
            ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/41+1B4y6qPL._SX381_BO1,204,203,200_.jpg",
            ISBN = "978-020163",
            AuthorId = erichGammaId,
            GenreId = programmingGenreId
        });

        var query = new GetFilteredBooksQuery
        {
            PageNumber = 1,
            PageSize = 10
        };

        var result = await SendAsync(query);

        result.Items.Should().HaveCount(2);
        result.TotalCount.Should().Be(2);
    }
}
