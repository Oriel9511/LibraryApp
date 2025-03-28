using LibraryApp.Application.Common.Models;
using LibraryApp.Application.Common.Models.Loans;
using LibraryApp.Application.Loans.Queries;

namespace LibraryApp.Web.Endpoints;

public class Loans : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization()
            .MapGet(GetLoansWithPagination);
    }

    public Task<PaginatedList<LoanDto>> GetLoansWithPagination(ISender sender, [AsParameters] GetLoansWithPaginationQuery query)
    {
        return sender.Send(query);
    }
}
