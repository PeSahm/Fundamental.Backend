using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IFinancialStatementReadRepository
{
    Task<FinancialStatement?> GetLastFinancialStatement(string isin, DateOnly date, CancellationToken cancellationToken = default);
}