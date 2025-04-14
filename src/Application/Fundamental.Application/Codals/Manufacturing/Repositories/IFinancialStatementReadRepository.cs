using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IFinancialStatementReadRepository
{
    Task<FinancialStatement?> GetLastFinancialStatement(string isin, DateOnly date, CancellationToken cancellationToken = default);
    Task<FinancialStatement?> GetLastFinancialStatement(string isin, FiscalYear year, StatementMonth month, CancellationToken cancellationToken = default);

}