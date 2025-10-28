using Ardalis.Specification;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class BalanceSheetDetailEntitySpec : Specification<BalanceSheetDetail>
{
    public static BalanceSheetDetailEntitySpec WhereBalanceSheet(ulong traceNo, uint fiscalYear, uint reportMonth)
    {
        BalanceSheetDetailEntitySpec spec = new();
        spec.Query.Where(x => x.BalanceSheet.FiscalYear.Year == fiscalYear)
            .Where(x => x.BalanceSheet.ReportMonth.Month == reportMonth)
            .Where(x => x.BalanceSheet.TraceNo == traceNo)
            .AsNoTracking();
        return spec;
    }
}