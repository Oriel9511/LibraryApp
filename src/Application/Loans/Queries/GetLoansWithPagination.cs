using LibraryApp.Application.Books.Queries.GetBooksWithPagination;
using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Mappings;
using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Loans;
using Microsoft.AspNetCore.Identity;

namespace LibraryApp.Application.Loans.Queries;

public class GetLoansWithPaginationQuery : IRequest<PaginatedList<LoanDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetLoansWithPaginationQueryHandler : IRequestHandler<GetLoansWithPaginationQuery, PaginatedList<LoanDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public GetLoansWithPaginationQueryHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }
    
    public async Task<PaginatedList<LoanDto>> Handle(GetLoansWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var users = _identityService.GetUsersAsQueryable();
        var query = from loan in _context.Loans
            join book in _context.Books on loan.BookId equals book.Id
            join user in users on loan.UserId equals user.Id
            select new LoanDto
            {
                Username = user.UserName,
                Email = user.Email,
                BookName = book.Title,
                ISBN = book.ISBN,
                LoanDate = loan.LoanDate,
                ReturnDate = loan.ReturnDate ?? new DateTime()
            };

        // Aplicamos ordenamiento y paginación
        return await query
            .OrderBy(l => l.LoanDate)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
