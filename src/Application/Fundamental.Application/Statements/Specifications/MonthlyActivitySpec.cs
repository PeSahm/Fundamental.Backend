using Ardalis.Specification;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

public sealed class MonthlyActivitySpec : Specification<MonthlyActivity>
{
    public MonthlyActivitySpec WhereTraceNo(ulong requestTraceNo)
    {
        Query.Where(x => x.TraceNo == requestTraceNo);
        return this;
    }

    public MonthlyActivitySpec WhereFiscalYear(ushort requestFiscalYear)
    {
        Query.Where(x => x.FiscalYear.Year == requestFiscalYear);
        return this;
    }

    public MonthlyActivitySpec WhereReportMonth(ushort requestReportMonth)
    {
        Query.Where(x => x.ReportMonth.Month == requestReportMonth);
        return this;
    }

    public MonthlyActivitySpec WhereSymbol(string isin)
    {
        Query.Where(x => x.Symbol.Isin == isin);
        return this;
    }

    public MonthlyActivitySpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }

    public MonthlyActivityResultItemSpec Select()
    {
        MonthlyActivityResultItemSpec select = new();

        foreach (WhereExpressionInfo<MonthlyActivity> whereExpression in WhereExpressions)
        {
            select.Query.Where(whereExpression.Filter);
        }

        return select;
    }
}