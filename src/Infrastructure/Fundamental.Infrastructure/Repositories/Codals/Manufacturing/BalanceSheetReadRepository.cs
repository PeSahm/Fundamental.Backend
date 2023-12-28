using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;
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

        var validStatements = await query
            .Where(x => x.ReportMonth.Month != 1)
            .GroupBy(gb => new { gb.Symbol.Isin, gb.FiscalYear, gb.ReportMonth })
            .Select(x => new
            {
                x.Key.Isin,
                TraceNo = x.Max(mx => mx.TraceNo),
                x.Key.FiscalYear,
                x.Key.ReportMonth
            }).ToPagingListAsync(request, "FiscalYear desc,ReportMonth desc ", cancellationToken);

        var result =
            await query
                .Where(q => validStatements.Items.Select(ss => ss.TraceNo).Contains(q.TraceNo))
                .Select(x => new
                {
                    x.Id,
                    x.Symbol.Isin,
                    Symbol = x.Symbol.Name,
                    x.Symbol.Title,
                    x.TraceNo,
                    x.Uri,
                    FiscalYear = x.FiscalYear.Year,
                    YearEndMonth = x.YearEndMonth.Month,
                    ReportMonth = x.ReportMonth.Month,
                    x.IsAudited,
                    x.Row,
                    x.CodalCategory,
                    x.CodalRow,
                    x.Value,
                    x.UpdatedAt,
                }).ToListAsync(cancellationToken: cancellationToken);

        List<GetBalanceSheetResultDto> mappedResult = new();

        foreach (var gb in validStatements.Items)
        {
            if (!result.Exists(x => x.TraceNo == gb.TraceNo))
            {
                continue;
            }

            List<GetBalanceSheetResultItem> items = result.Where(x => x.TraceNo == gb.TraceNo).Select(x => new GetBalanceSheetResultItem
            {
                Order = x.Row,
                CodalRow = x.CodalRow,
                Category = x.CodalCategory,
                Value = (CodalMoney)x.Value,
            }).OrderBy(o => o.Order).ToList();

            mappedResult.Add(new GetBalanceSheetResultDto
            {
                Isin = gb.Isin,
                Symbol = result[0].Symbol,
                TraceNo = gb.TraceNo,
                Uri = result[0].Uri,
                FiscalYear = gb.FiscalYear,
                YearEndMonth = result[0].YearEndMonth,
                ReportMonth = gb.ReportMonth,
                IsAudited = result[0].IsAudited,
                Items = items,
            });
        }

        return new Paginated<GetBalanceSheetResultDto>(mappedResult, validStatements.Meta);
    }
}