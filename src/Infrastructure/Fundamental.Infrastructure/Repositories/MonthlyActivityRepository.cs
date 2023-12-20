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

        return await query.Select(x => new GetMonthlyActivitiesResultItem
            {
                Id = x.Id,
                Isin = x.Symbol.Isin,
                Symbol = x.Symbol.Name,
                Title = x.Symbol.Title,
                Uri = x.Uri,
                FiscalYear = x.FiscalYear,
                YearEndMonth = x.YearEndMonth,
                ReportMonth = x.ReportMonth,
                SaleBeforeCurrentMonth = (CodalMoney)x.SaleBeforeCurrentMonth,
                SaleCurrentMonth = (CodalMoney)x.SaleCurrentMonth,
                SaleIncludeCurrentMonth = (CodalMoney)x.SaleIncludeCurrentMonth,
                SaleLastYear = (CodalMoney)x.SaleLastYear,
                HasSubCompanySale = x.HasSubCompanySale,
                TraceNo = x.TraceNo,
                CreatedAt = x.CreatedAt
            })
            .ToPagingListAsync(request, "CreatedAt desc", cancellationToken);
    }
}