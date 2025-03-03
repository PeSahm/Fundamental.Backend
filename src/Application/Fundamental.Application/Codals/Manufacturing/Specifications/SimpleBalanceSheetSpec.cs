using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class SimpleBalanceSheetSpec : Specification<BalanceSheet, SimpleBalanceSheet>
{
    public SimpleBalanceSheetSpec()
    {
        Query.AsNoTracking();
        Query.Select(x => new SimpleBalanceSheet
        {
            Isin = x.Symbol.Isin,
            TraceNo = x.TraceNo,
            FiscalYear = x.FiscalYear,
            ReportMonth = x.ReportMonth,
            YearEndMonth = x.YearEndMonth,
        });
    }

    public SimpleBalanceSheetSpec SetTop(int top)
    {
        Query.Take(top);
        return this;
    }
}