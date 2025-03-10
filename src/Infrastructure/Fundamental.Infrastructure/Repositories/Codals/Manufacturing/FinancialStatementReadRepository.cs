using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
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
}