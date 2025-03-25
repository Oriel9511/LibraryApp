using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Genres;
using LibraryApp.Application.Genres.Commands.CreateGenre;
using LibraryApp.Application.Genres.Commands.DeleteGenre;
using LibraryApp.Application.Genres.Commands.UpdateGenre;
using LibraryApp.Application.Genres.Queries.GetGenres;

namespace LibraryApp.Web.Endpoints;

public class Genres : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetGenresWithPagination)
            .MapPost(CreateGenre)
            .MapPut(UpdateGenre, "{id}")
            .MapDelete(DeleteGenre, "{id}");
    }

    public Task<List<GenreDto>> GetGenresWithPagination(ISender sender, [AsParameters] GetGenresQuery query)
    {
        return sender.Send(query);
    }

    public Task<int> CreateGenre(ISender sender, CreateGenreCommand command)
    {
        return sender.Send(command);
    }

    public async Task<IResult> UpdateGenre(ISender sender, int id, UpdateGenreCommand command)
    {
        if (id != command.Id) return Results.BadRequest();
        await sender.Send(command);
        return Results.NoContent();
    }

    public async Task<IResult> DeleteGenre(ISender sender, int id)
    {
        await sender.Send(new DeleteGenreCommand(id));
        return Results.NoContent();
    }
}
