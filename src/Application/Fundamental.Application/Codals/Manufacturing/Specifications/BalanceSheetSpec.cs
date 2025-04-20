using Ardalis.Specification;
using Fundamental.BuildingBlock.Extensions;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class BalanceSheetSpec : Specification<BalanceSheet>
{
    public static BalanceSheetSpec WithTraceNo(ulong traceNo)
    {
        BalanceSheetSpec spec = new();
        spec.Query
            .Where(x => x.TraceNo == traceNo)
            .AsNoTracking();
        return spec;
    }

    public static BalanceSheetSpec Where(ulong traceNo, string isin, uint fiscalYear, uint yearEndMonth, uint reportMonth)
    {
        BalanceSheetSpec spec = new();
        spec.Query
            .Where(x => x.Symbol.Isin == isin)
            .Where(x => x.FiscalYear.Year == fiscalYear)
            .Where(x => x.ReportMonth.Month == reportMonth)
            .Where(x => x.TraceNo < traceNo)
            .AsNoTracking();
        return spec;
    }

    public static BalanceSheetSpec Where(ulong traceNo, uint fiscalYear, uint yearEndMonth, uint reportMonth)
    {
        BalanceSheetSpec spec = new();
        spec.Query
            .Where(x => x.FiscalYear.Year == fiscalYear)
            .Where(x => x.ReportMonth.Month == reportMonth)
            .Where(x => x.YearEndMonth.Month == yearEndMonth)
            .Where(x => x.TraceNo == traceNo)
            .AsNoTracking();
        return spec;
    }

    public BalanceSheetSpec WhereNoFinancialStatement()
    {
        Query
            .Where(x => x.FinancialStatement == null)
            .AsNoTracking();
        return this;
    }

    public SimpleBalanceSheetSpec ToSimpleBalanceSheetSpec()
    {
        SimpleBalanceSheetSpec select = new();
        select.LoadSpecification(this);
        return select;
    }
}