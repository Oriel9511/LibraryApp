namespace LibraryApp.Application.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(v => v.Title)
            .MaximumLength(200)
            .NotEmpty();
        RuleFor(v => v.Resume)
            .MaximumLength(2000)
            .NotEmpty();
        RuleFor(v => v.PublicationYear)
            .GreaterThan(0);
        RuleFor(v => v.ISBN)
            .MaximumLength(13)
            .NotEmpty();
    }
}
