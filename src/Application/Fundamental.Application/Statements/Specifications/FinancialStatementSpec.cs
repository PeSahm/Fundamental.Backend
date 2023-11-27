using Ardalis.Specification;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

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
}