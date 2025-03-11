using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public sealed class BalanceSheetReadRepository(FundamentalDbContext dbContext) : IBalanceSheetReadRepository
{
    public async Task<Paginated<GetBalanceSheetResultDto>> GetBalanceSheet(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<BalanceSheet> query = dbContext.BalanceSheets.Where(x => x.Description != null).AsNoTracking();

        if (request is { IsinList: not null } && request.IsinList.Count != 0)
        {
            query = query.Where(x => request.IsinList.Contains(x.Symbol.Isin));
        }

        if (request.FiscalYear.HasValue)
        {
            query = query.Where(x => x.FiscalYear.Year == request.FiscalYear);
        }

        if (request.ReportMonth.HasValue)
        {
            query = query.Where(x => x.ReportMonth.Month == request.ReportMonth);
        }

        if (request.TraceNo.HasValue)
        {
            query = query.Where(x => x.TraceNo == request.TraceNo);
        }

        Paginated<GetBalanceSheetResultDto> validStatements = await query
            .Where(x => x.ReportMonth.Month != 1)
            .GroupBy(gb => new { gb.Symbol.Isin, FiscalYear = gb.FiscalYear.Year, ReportMonth = gb.ReportMonth.Month })
            .Select(x => new GetBalanceSheetResultDto
            {
                Isin = x.Key.Isin,
                TraceNo = x.Max(mx => mx.TraceNo),
                FiscalYear = x.Key.FiscalYear,
                ReportMonth = x.Key.ReportMonth,
                IsAudited = x.First().IsAudited,
                YearEndMonth = x.First().YearEndMonth.Month,
                Symbol = x.First().Symbol.Name,
                Uri = x.First().Uri,
            }).ToPagingListAsync(request, "FiscalYear desc,ReportMonth desc", cancellationToken);

        return validStatements;
    }

    public Task<List<SimpleBalanceSheet>> GetLastBalanceSheetDetails(CancellationToken cancellationToken = default)
    {
        return dbContext.BalanceSheets
                .AsNoTracking()
                .Where(x => !dbContext.ManufacturingFinancialStatement.Any(fs => fs.TraceNo == x.TraceNo))
                    .GroupBy(gb => new { gb.Symbol.Isin, FiscalYear = gb.FiscalYear.Year, ReportMonth = gb.ReportMonth.Month })
                    .Select(x => new SimpleBalanceSheet
                    {
                        Isin = x.Key.Isin,
                        TraceNo = x.Max(mx => mx.TraceNo),
                        FiscalYear = x.Key.FiscalYear,
                        ReportMonth = x.Key.ReportMonth,
                        YearEndMonth = x.First().YearEndMonth.Month,
                    })
                    .ToListAsync(cancellationToken)
            ;
    }
}