using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class MonthlyActivityRepository : IMonthlyActivityRepository
{
    private readonly FundamentalDbContext _dbContext;

    public MonthlyActivityRepository(FundamentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Paginated<GetMonthlyActivitiesResultItem>> GetMonthlyActivitiesAsync(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<MonthlyActivity> query = _dbContext.MonthlyActivities.AsNoTracking();

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

    public Task<MonthlyActivity?> GetLastMonthlyActivity(string isin, FiscalYear fiscalYear, CancellationToken cancellationToken)
    {
        return _dbContext.MonthlyActivities
            .AsNoTracking()
            .Where(x => x.Symbol.Isin == isin)
            .Where(x => x.FiscalYear.Year == fiscalYear.Year)
            .OrderByDescending(x => x.ReportMonth.Month).ThenByDescending(x => x.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }
}