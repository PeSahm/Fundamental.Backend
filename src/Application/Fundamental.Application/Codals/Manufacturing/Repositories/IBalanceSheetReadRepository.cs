using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IBalanceSheetReadRepository
{
    Task<Paginated<GetBalanceSheetResultDto>> GetBalanceSheet(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken = default
    );

    Task<List<SimpleBalanceSheet>> GetLastBalanceSheetDetails(
        CancellationToken cancellationToken = default
    );
}