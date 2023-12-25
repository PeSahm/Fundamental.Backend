using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IIncomeStatementsReadRepository
{
    Task<Paginated<GetIncomeStatementsResultDto>> GetIncomeStatements(
        GetIncomeStatementsRequest request,
        CancellationToken cancellationToken = default
    );
}