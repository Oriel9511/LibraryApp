using System.Security.Authentication;
using LibraryApp.Application.User.Commands.GetJwt;
using LibraryApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Endpoints;

public class Users : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .AllowAnonymous()
            .MapPost(GetJwt, "GetJwt")
            .MapIdentityApi<ApplicationUser>();
    }

    public async Task<IResult> GetJwt(ISender sender, [FromBody] GetJwtCommand command)
    {
        try
        {
            var token = await sender.Send(command);
            return Results.Ok(new {
                token
            });
        }
        catch (AuthenticationException)
        {
            return Results.Unauthorized();
        }
    }
}
