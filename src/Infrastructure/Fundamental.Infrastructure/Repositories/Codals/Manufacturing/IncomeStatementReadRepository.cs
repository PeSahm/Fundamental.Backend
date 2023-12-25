using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public sealed class IncomeStatementReadRepository(FundamentalDbContext dbContext) : IIncomeStatementsReadRepository
{
    public async Task<Paginated<GetIncomeStatementsResultDto>> GetIncomeStatements(
        GetIncomeStatementsRequest request,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<IncomeStatement> query = dbContext.IncomeStatements.Where(x => x.Description != null).AsNoTracking();

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

        var result = await query.Select(x => new
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
            })
            .ToPagingListAsync(request, "TraceNo desc", cancellationToken);

        List<GetIncomeStatementsResultDto> mappedResult = result.Items.GroupBy(gb => new
                { gb.Isin, gb.TraceNo, gb.FiscalYear, gb.ReportMonth, })
            .Select(x => new GetIncomeStatementsResultDto
            {
                Id = x.First().Id,
                Isin = x.Key.Isin,
                Symbol = x.First().Symbol,
                TraceNo = x.Key.TraceNo,
                Uri = x.First().Uri,
                FiscalYear = x.Key.FiscalYear,
                YearEndMonth = x.First().YearEndMonth,
                ReportMonth = x.Key.ReportMonth,
                IsAudited = x.First().IsAudited,
                Items = x.Select(y => new GetIncomeStatementsResultItem()
                {
                    Order = y.Row,
                    CodalRow = y.CodalRow,
                    Value = (CodalMoney)y.Value,
                }).OrderBy(o => o.Order).ToList(),
            }).ToList();

        return new Paginated<GetIncomeStatementsResultDto>(mappedResult, result.Meta);
    }
}