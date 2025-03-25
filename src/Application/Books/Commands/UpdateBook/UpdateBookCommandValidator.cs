namespace LibraryApp.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
{
    public UpdateBookCommandValidator()
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
            .NotEmpty();
    }
}
