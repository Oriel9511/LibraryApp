using System.Security.Authentication;
using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.User.Commands.GetJwt;

public record GetJwtCommand : IRequest<string>
{
    public string? Email { get; set; }
    public string? Password { get; set; }
}

public class GetJwtCommandHandler : IRequestHandler<GetJwtCommand, string>
{
    private readonly IJwtService jwtService;

    public GetJwtCommandHandler(IJwtService jwtService)
    {
        this.jwtService = jwtService;
    }

    public async Task<string> Handle(GetJwtCommand request, CancellationToken cancellationToken)
    {
        if (request.Email == null || request.Password == null)
            throw new AuthenticationException("Email and password are required");
        return await jwtService.CreateToken(request.Email, request.Password);
    }
}
