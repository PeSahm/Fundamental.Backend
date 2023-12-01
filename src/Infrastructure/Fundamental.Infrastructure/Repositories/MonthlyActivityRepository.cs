using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.Application.Statements.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.Entities;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories;

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

        return await query.Select(x => new GetMonthlyActivitiesResultItem(
            x.Symbol.Isin,
            x.Symbol.Name,
            x.Symbol.Title,
            x.Uri,
            x.FiscalYear,
            x.YearEndMonth,
            x.ReportMonth,
            (CodalMoney)x.SaleBeforeCurrentMonth,
            (CodalMoney)x.SaleCurrentMonth,
            (CodalMoney)x.SaleIncludeCurrentMonth,
            (CodalMoney)x.SaleLastYear,
            x.HasSubCompanySale
        )).ToPagingListAsync(request, cancellationToken);
    }
}