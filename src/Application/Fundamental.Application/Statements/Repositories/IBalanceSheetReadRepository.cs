using Fundamental.Application.Statements.Queries.GetBalanceSheets;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Statements.Repositories;

public interface IBalanceSheetReadRepository
{
    Task<Paginated<GetBalanceSheetResultDto>> GetBalanceSheet(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken = default
    );
}