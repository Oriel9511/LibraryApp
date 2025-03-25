namespace LibraryApp.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Nationality)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Birthday)
            .NotEmpty();
    }
}
