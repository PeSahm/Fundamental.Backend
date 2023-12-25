using Ardalis.Specification;
using Fundamental.Domain.Codals;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class FinancialStatementSpec : Specification<FinancialStatement>
{
    public FinancialStatementSpec WhereTraceNo(ulong requestTraceNo)
    {
        Query.Where(x => x.TraceNo == requestTraceNo);
        return this;
    }

    public FinancialStatementSpec WhereFiscalYear(ushort requestFiscalYear)
    {
        Query.Where(x => x.FiscalYear.Year == requestFiscalYear);
        return this;
    }

    public FinancialStatementSpec WhereReportMonth(ushort requestReportMonth)
    {
        Query.Where(x => x.ReportMonth.Month == requestReportMonth);
        return this;
    }

    public FinancialStatementSpec WhereSymbol(string isin)
    {
        Query.Where(x => x.Symbol.Isin == isin);
        return this;
    }

    public FinancialStatementResultItemSpec Select()
    {
        FinancialStatementResultItemSpec select = new();

        foreach (WhereExpressionInfo<FinancialStatement> whereExpression in WhereExpressions)
        {
            select.Query.Where(whereExpression.Filter);
        }

        return select;
    }

    public FinancialStatementSpec WhereId(Guid requestId)
    {
        Query.Where(x => x.Id == requestId);
        return this;
    }

    public FinancialStatementSpec WhereIdNot(Guid requestId)
    {
        Query.Where(x => x.Id != requestId);
        return this;
    }
}