﻿using System.Security.Authentication;
using LibraryApp.Application.User.Commands.GetJwt;
using LibraryApp.Application.User.Commands.GetPassword;
using LibraryApp.Application.User.Commands.Register;
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
            .MapPost(RegisterUser, "Register")
            .MapPost(GetPassword, "GetPassword");
        // .MapIdentityApi<ApplicationUser>();
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
    
    public async Task<IResult> RegisterUser(ISender sender, [FromBody] RegisterCommand command)
    {
        var result = await sender.Send(command);
        if (result != null) return Results.Ok();
        return Results.BadRequest("Failed to register user. Verify the password is strong enough.");
    }

    public async Task<string> GetPassword(ISender sender, [FromBody] GetPasswordCommand command)
    {
        return await sender.Send(command);
    }
}
