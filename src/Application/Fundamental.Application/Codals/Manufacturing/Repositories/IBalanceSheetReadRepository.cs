using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IBalanceSheetReadRepository
{
    Task<Paginated<GetBalanceSheetResultDto>> GetBalanceSheet(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken = default
    );
}