using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class MonthlyActivityResultItemSpec : Specification<CanonicalMonthlyActivity, GetMonthlyActivitiesResultItem>
{
    public MonthlyActivityResultItemSpec()
    {
        Query.AsNoTracking();
        Query.Select(x => new GetMonthlyActivitiesResultItem
        {
            Id = x.Id,
            Isin = x.Symbol.Isin,
            Symbol = x.Symbol.Name,
            Title = x.Symbol.Title,
            Uri = x.Uri,
            Version = x.Version,
            FiscalYear = x.FiscalYear,
            YearEndMonth = x.YearEndMonth,
            ReportMonth = x.ReportMonth,
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