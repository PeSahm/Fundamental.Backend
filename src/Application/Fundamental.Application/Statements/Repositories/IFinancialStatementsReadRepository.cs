using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Statements.Repositories;

public interface IFinancialStatementsReadRepository
{
    Task<Paginated<GetFinancialStatementsResultItem>> GetFinancialStatementsAsync(
        GetFinancialStatementsRequest request,
        CancellationToken cancellationToken = default
    );
}