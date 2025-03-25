namespace LibraryApp.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(v => v.Name)
            .MaximumLength(200);
        RuleFor(v => v.Nationality)
            .MaximumLength(200);
    }
}
