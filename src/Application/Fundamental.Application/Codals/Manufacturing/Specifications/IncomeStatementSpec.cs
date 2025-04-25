using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class IncomeStatementSpec : Specification<IncomeStatement>
{
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

    public IncomeStatementSpec WithTraceNo(ulong traceNo)
    {
        Query
            .Where(x => x.TraceNo == traceNo)
            .AsNoTracking();
        return this;
    }

    public IncomeStatementSpec WhereFiscalYear(FiscalYear fiscalYear)
    {
        Query
            .Where(x => x.FiscalYear.Year == fiscalYear.Year)
            .AsNoTracking();
        return this;
    }

    public IncomeStatementSpec WhereReportMonth(StatementMonth statementMonth)
    {
        Query
            .Where(x => x.ReportMonth.Month == statementMonth.Month)
            .AsNoTracking();
        return this;
    }

    public IncomeStatementSpec WhereYearEndMonth(StatementMonth yearEndMonth)
    {
        Query
            .Where(x => x.YearEndMonth.Month == yearEndMonth.Month)
            .AsNoTracking();
        return this;
    }

    public IncomeStatementSpec WhereIsin(string isin)
    {
        Query
            .Where(x => x.Symbol.Isin == isin)
            .AsNoTracking();
        return this;
    }

    public IncomeStatementSpec WhereIncomeStatementRow(ushort row)
    {
        Query
            .Where(x => x.CodalRow == row)
            .AsNoTracking();
        return this;
    }
}