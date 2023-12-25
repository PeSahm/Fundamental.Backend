using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IFinancialStatementsReadRepository
{
    Task<Paginated<GetFinancialStatementsResultItem>> GetFinancialStatementsAsync(
        GetFinancialStatementsRequest request,
        CancellationToken cancellationToken = default
    );
}