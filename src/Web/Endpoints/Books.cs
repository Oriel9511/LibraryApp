using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Books.Commands.CreateBook;
using LibraryApp.Application.Books.Commands.DeleteBook;
using LibraryApp.Application.Books.Commands.UpdateBook;
using LibraryApp.Application.Books.Queries.GetBooksWithPagination;
using LibraryApp.Application.Common.Models.Books;

namespace LibraryApp.Web.Endpoints;

public class Books : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetBooksWithPagination)
            .MapPost(CreateBook)
            .MapPut(UpdateBook, "{id}")
            .MapDelete(DeleteBook, "{id}");
    }

    public Task<PaginatedList<BookDto>> GetBooksWithPagination(ISender sender, [AsParameters] GetBooksWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateBook(ISender sender, CreateBookCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateBook(ISender sender, int id, UpdateBookCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteBook(ISender sender, int id)
    {
        await sender.Send(new DeleteBookCommand(id));
        return Results.NoContent();
    }
}
