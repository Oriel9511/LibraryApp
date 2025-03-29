using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Loans;
using LibraryApp.Application.Loans.Commands.CreateLoan;
using LibraryApp.Application.Loans.Queries;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Web.Endpoints;

public class Loans : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetLoansWithPagination)
            .MapPost(CreateLoan);
    }

    public Task<PaginatedList<LoanDto>> GetLoansWithPagination(ISender sender, [AsParameters] GetLoansWithPaginationQuery query)
    {
        return sender.Send(query);
    }

    public async Task<int> CreateLoan(ISender sender, [FromBody] CreateLoanCommand command)
    {
        return await sender.Send(command);
    }
}
