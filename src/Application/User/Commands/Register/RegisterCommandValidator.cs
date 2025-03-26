namespace LibraryApp.Application.User.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(v => v.Email)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Password)
            .MaximumLength(200)
            .NotEmpty();
    }
}
