using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Application.Loans.Commands.CreateLoan;

public class CreateLoanCommand : IRequest<int>
{
    public string? UserId { get; set; }
    public int BookId { get; set; }
}

public class CreateLoanCommandHandler : IRequestHandler<CreateLoanCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateLoanCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateLoanCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
        
        try
        {
            var book = await _context.Books
                .FirstOrDefaultAsync(b => b.Id == request.BookId, cancellationToken);

            if (book == null)
            {
                throw new Exception($"Book with ID {request.BookId} not found");
            }

            if (book.Stock <= 0)
            {
                throw new Exception($"Book with ID {request.BookId} is out of stock");
            }

            var loan = new Loan
            {
                UserId = request.UserId,
                BookId = request.BookId,
                LoanDate = DateTime.UtcNow,
                ReturnDate = null
            };

            book.Stock--;
            _context.Loans.Add(loan);

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return loan.Id;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
