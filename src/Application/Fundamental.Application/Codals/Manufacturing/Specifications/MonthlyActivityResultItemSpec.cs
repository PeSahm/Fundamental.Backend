using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class MonthlyActivityResultItemSpec : Specification<CanonicalMonthlyActivity, GetMonthlyActivityDetailItem>
{
    public MonthlyActivityResultItemSpec()
    {
        Query.AsNoTracking();
        Query.Include(x => x.ProductionAndSalesItems);
        Query.Include(x => x.BuyRawMaterialItems);
        Query.Include(x => x.EnergyItems);
        Query.Include(x => x.CurrencyExchangeItems);
        Query.Include(x => x.Descriptions);
        Query.Select(x => new GetMonthlyActivityDetailItem
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
        });
    }

    public MonthlyActivityResultItemSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }

    public MonthlyActivityResultItemSpec WhereSymbol(string isin)
    {
        Query.Where(x => x.Symbol.Isin == isin);
        return this;
    }

    public MonthlyActivityResultItemSpec WhereFiscalYear(ushort fiscalYear)
    {
        Query.Where(x => x.FiscalYear.Year == fiscalYear);
        return this;
    }

    public MonthlyActivityResultItemSpec WhereReportMonth(ushort reportMonth)
    {
        Query.Where(x => x.ReportMonth.Month == reportMonth);
        return this;
    }

    public MonthlyActivityResultItemSpec WhereTraceNo(ulong traceNo)
    {
        Query.Where(x => x.TraceNo == traceNo);
        return this;
    }
}