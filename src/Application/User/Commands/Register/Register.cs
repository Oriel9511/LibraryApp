using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Models;

namespace LibraryApp.Application.User.Commands.Register;

public class RegisterCommand : IRequest<(Result Result, string UserId)>
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, (Result Result, string UserId)>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(IIdentityService identityService, IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task<(Result Result, string UserId)> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.CreateUserAsync(request.Email!, request.Password!);
        await _emailService.SendEmailAsync(request.Email!, "Welcome!", "Thank you for registering!");
        
        return response;
    }
}
