using LibraryApp.Application.Common.Interfaces;

namespace LibraryApp.Application.User.Commands.GetPassword;

public class GetPasswordCommand : IRequest<string>
{
    public string? Email { get; init; }
}

public class GetPasswordCommandHandler : IRequestHandler<GetPasswordCommand, string>
{
    private readonly IIdentityService _identityService;

    public GetPasswordCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<string> Handle(GetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.GetPassword(request.Email!);
    }
}
