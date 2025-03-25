using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Authors.Commands.CreateAuthor;
using LibraryApp.Application.Authors.Commands.DeleteAuthor;
using LibraryApp.Application.Authors.Commands.UpdateAuthor;
using LibraryApp.Application.Authors.Queries.GetAuthorsWithPagination;
using LibraryApp.Application.Common.Models.Authors;

namespace LibraryApp.Web.Endpoints;

public class Authors : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetAuthorsWithPagination)
            .MapPost(CreateAuthor)
            .MapPut(UpdateAuthor, "{id}")
            .MapDelete(DeleteAuthor, "{id}");
    }

    public Task<PaginatedList<AuthorDto>> GetAuthorsWithPagination(ISender sender, [AsParameters] GetAuthorsWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateAuthor(ISender sender, CreateAuthorCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateAuthor(ISender sender, int id, UpdateAuthorCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteAuthor(ISender sender, int id)
    {
        await sender.Send(new DeleteAuthorCommand(id));
        return Results.NoContent();
    }
}
