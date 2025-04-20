using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class IncomeStatementSpec : Specification<IncomeStatement>
{
    public static IncomeStatementSpec WithTraceNo(ulong traceNo)
    {
        IncomeStatementSpec spec = new();
        spec.Query
            .Where(x => x.TraceNo == traceNo)
            .AsNoTracking();
        return spec;
    }

    public static IncomeStatementSpec Where(ulong traceNo, string isin, uint fiscalYear, uint yearEndMonth, uint reportMonth)
    {
        IncomeStatementSpec spec = new();
        spec.Query
            .Where(x => x.Symbol.Isin == isin)
            .Where(x => x.FiscalYear.Year == fiscalYear)
            .Where(x => x.YearEndMonth.Month == yearEndMonth)
            .Where(x => x.ReportMonth.Month == reportMonth)
            .Where(x => x.TraceNo < traceNo)
            .AsNoTracking();
        return spec;
    }

    public static IncomeStatementSpec Where(ulong traceNo, uint fiscalYear, uint yearEndMonth, uint reportMonth)
    {
        IncomeStatementSpec spec = new();
        spec.Query
            .Where(x => x.FiscalYear.Year == fiscalYear)
            .Where(x => x.YearEndMonth.Month == yearEndMonth)
            .Where(x => x.ReportMonth.Month == reportMonth)
            .Where(x => x.TraceNo == traceNo)
            .AsNoTracking();
        return spec;
    }
}