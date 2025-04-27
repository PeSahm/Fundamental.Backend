using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class FinancialStatementReadRepository(FundamentalDbContext dataContent) : IFinancialStatementReadRepository
{
    public async Task<FinancialStatement?> GetLastFinancialStatement(
        string isin,
        DateOnly date,
        CancellationToken cancellationToken = default
    )
    {
        int persianYear = date.GetPersianYear();
        int persianMonth = date.GetPersianMonth();

        return await dataContent.ManufacturingFinancialStatement
            .Where(x => x.Symbol.Isin == isin && x.FiscalYear.Year <= persianYear && x.ReportMonth.Month <= persianMonth)
            .OrderByDescending(x => x.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<FinancialStatement?> GetLastFinancialStatement(
        string isin,
        FiscalYear year,
        StatementMonth month,
        CancellationToken cancellationToken = default
    )
    {
        return dataContent.ManufacturingFinancialStatement
            .Where(x => x.Symbol.Isin == isin && x.FiscalYear.Year <= year.Year)
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(x => x.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }
}