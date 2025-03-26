using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Models;

namespace LibraryApp.Application.User.Commands.Register;

public class RegisterCommand : IRequest<string?>
{
    public string? Email { get; init; }
    public string? Password { get; init; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, string?>
{
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(IIdentityService identityService, IEmailService emailService)
    {
        _identityService = identityService;
        _emailService = emailService;
    }

    public async Task<string?> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var response = await _identityService.CreateUserAsync(request.Email!, request.Password!);
        if (response != null)
        {
            await _emailService.SendEmailAsync(request.Email!, "Welcome!", "Thank you for registering!");
        }
        
        return response;
    }
}
