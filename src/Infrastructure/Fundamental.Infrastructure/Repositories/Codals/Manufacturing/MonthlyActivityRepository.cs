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
    /// <summary>
    /// Retrieves a paginated list of monthly activity items filtered and projected according to the request.
    /// </summary>
    /// <param name="request">Filter and paging options. Supported filters: IsinList (matches Symbol.Isin), Year (FiscalYear.Year), ReportMonth (ReportMonth.Month); paging/sorting are applied from the request.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the query.</param>
    /// <returns>A paginated list of <see cref="GetMonthlyActivitiesListItem"/> containing the projected fields for matching canonical monthly activities.</returns>
    public async Task<Paginated<GetMonthlyActivitiesListItem>> GetMonthlyActivitiesAsync(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<CanonicalMonthlyActivity> query = dbContext.CanonicalMonthlyActivities
            .AsNoTracking();

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

        return await query.Select(x => new GetMonthlyActivitiesListItem
            {
                Id = x.Id,
                Isin = x.Symbol.Isin,
                Symbol = x.Symbol.Name,
                Title = x.Symbol.Title,
                Uri = x.Uri,
                Version = x.Version,
                FiscalYear = x.FiscalYear.Year,
                YearEndMonth = x.YearEndMonth.Month,
                ReportMonth = x.ReportMonth.Month,
                HasSubCompanySale = x.HasSubCompanySale,
                TraceNo = x.TraceNo,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToPagingListAsync(request, "UpdatedAt desc", cancellationToken);
    }

    public Task<CanonicalMonthlyActivity?> GetFirstMonthlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    )
    {
        return dbContext.CanonicalMonthlyActivities
            .AsNoTracking()
            .Where(sale => sale.Symbol.Isin == isin)
            .Where(sale => (sale.FiscalYear.Year == fiscalYear.Year && sale.ReportMonth.Month >= month.Month) ||
                           (sale.FiscalYear.Year > fiscalYear.Year))
            .OrderBy(sale => sale.FiscalYear.Year)
            .ThenBy(x => x.ReportMonth.Month)
            .ThenByDescending(sale => sale.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieve a canonical monthly activity matching the specified ISIN, fiscal year, and report month.
    /// </summary>
    /// <param name="isin">ISIN of the security to match.</param>
    /// <param name="fiscalYear">Fiscal year to match (compares FiscalYear.Year).</param>
    /// <param name="month">Report month to match (compares ReportMonth.Month).</param>
    /// <param name="cancellationToken">Token to cancel the database query.</param>
    /// <returns>The matching <see cref="CanonicalMonthlyActivity"/>, or <c>null</c> if none is found.</returns>
    public Task<CanonicalMonthlyActivity?> GetMonthlyActivity(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth month,
        CancellationToken cancellationToken
    )
    {
        return dbContext.CanonicalMonthlyActivities
            .AsNoTracking()
            .Where(sale => sale.Symbol.Isin == isin)
            .Where(sale => sale.FiscalYear.Year == fiscalYear.Year)
            .Where(sale => sale.ReportMonth.Month == month.Month)
            .OrderByDescending(sale => sale.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(sale => sale.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }
}