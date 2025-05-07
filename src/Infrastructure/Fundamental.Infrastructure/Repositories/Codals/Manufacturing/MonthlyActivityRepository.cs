using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class MonthlyActivityRepository(FundamentalDbContext dbContext) : IMonthlyActivityRepository
{
    public async Task<Paginated<GetMonthlyActivitiesResultItem>> GetMonthlyActivitiesAsync(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<MonthlyActivity> query = dbContext.MonthlyActivities.AsNoTracking();

        if (request is { IsinList: not null } && request.IsinList.Length != 0)
        {
            query = query.Where(x => request.IsinList.Contains(x.Symbol.Isin));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(x => x.FiscalYear.Year == request.Year);
        }

        if (request.ReportMonth.HasValue)
        {
            query = query.Where(x => x.ReportMonth.Month == request.ReportMonth);
        }

        return await query.Select(x => new GetMonthlyActivitiesResultItem
            {
                Id = x.Id,
                Isin = x.Symbol.Isin,
                Symbol = x.Symbol.Name,
                Title = x.Symbol.Title,
                Uri = x.Uri,
                FiscalYear = x.FiscalYear.Year,
                YearEndMonth = x.YearEndMonth.Month,
                ReportMonth = x.ReportMonth.Month,
                SaleBeforeCurrentMonth = x.SaleBeforeCurrentMonth.Value,
                SaleCurrentMonth = x.SaleCurrentMonth.Value,
                SaleIncludeCurrentMonth = x.SaleIncludeCurrentMonth.Value,
                SaleLastYear = x.SaleLastYear.Value,
                HasSubCompanySale = x.HasSubCompanySale,
                TraceNo = x.TraceNo,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToPagingListAsync(request, "UpdatedAt desc", cancellationToken);
    }

    public Task<MonthlyActivity?> GetFirstMonthlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    )
    {
        return dbContext.MonthlyActivities
            .AsNoTracking()
            .Where(sale => sale.Symbol.Isin == isin)
            .Where(sale => (sale.FiscalYear.Year == fiscalYear.Year && sale.ReportMonth.Month >= month.Month) ||
                           (sale.FiscalYear.Year > fiscalYear.Year))
            .OrderBy(sale => sale.FiscalYear.Year)
            .ThenBy(x => x.ReportMonth.Month)
            .ThenByDescending(sale => sale.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<MonthlyActivity?> GetMonthlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    )
    {
        return dbContext.MonthlyActivities
            .AsNoTracking()
            .Where(sale => sale.Symbol.Isin == isin)
            .Where(sale => (sale.FiscalYear.Year == fiscalYear.Year))
            .Where(sale => sale.ReportMonth.Month == month.Month)
            .OrderByDescending(sale => sale.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(sale => sale.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }
}