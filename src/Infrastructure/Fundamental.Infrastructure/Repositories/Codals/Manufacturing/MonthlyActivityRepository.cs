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
        IQueryable<CanonicalMonthlyActivity> query = dbContext.CanonicalMonthlyActivities
            .AsNoTracking()
            .Include(x => x.ProductionAndSalesItems)
            .Include(x => x.BuyRawMaterialItems)
            .Include(x => x.EnergyItems)
            .Include(x => x.CurrencyExchangeItems)
            .Include(x => x.Descriptions);

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
                Version = x.Version,
                FiscalYear = x.FiscalYear.Year,
                YearEndMonth = x.YearEndMonth.Month,
                ReportMonth = x.ReportMonth.Month,
                HasSubCompanySale = x.HasSubCompanySale,
                TraceNo = x.TraceNo,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt,
                ProductionAndSalesItems = x.ProductionAndSalesItems,
                BuyRawMaterialItems = x.BuyRawMaterialItems,
                EnergyItems = x.EnergyItems,
                CurrencyExchangeItems = x.CurrencyExchangeItems,
                Descriptions = x.Descriptions
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
            .Where(sale => (sale.FiscalYear.Year == fiscalYear.Year))
            .Where(sale => sale.ReportMonth.Month == month.Month)
            .OrderByDescending(sale => sale.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(sale => sale.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }
}