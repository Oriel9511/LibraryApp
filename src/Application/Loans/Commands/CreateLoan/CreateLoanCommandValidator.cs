namespace LibraryApp.Application.Loans.Commands.CreateLoan;

public class CreateLoanCommandValidator : AbstractValidator<CreateLoanCommand>
{
    public CreateLoanCommandValidator()
    {
        RuleFor(v => v.UserId)
            .NotEmpty()
            .WithMessage("UserId is required.");

        RuleFor(v => v.BookId)
            .GreaterThan(0)
            .WithMessage("BookId must be greater than 0.");
    }
}
