using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class BalanceSheetDetailSpec : Specification<BalanceSheetDetail, GetBalanceSheetDetailResultDto>
{
    public static BalanceSheetDetailSpec Where(ulong traceNo, uint fiscalYear, uint reportMonth)
    {
        BalanceSheetDetailSpec spec = new();

        spec.Query.Where(x => x.BalanceSheet.FiscalYear.Year == fiscalYear)
            .Where(x => x.BalanceSheet.ReportMonth.Month == reportMonth)
            .Where(x => x.BalanceSheet.TraceNo == traceNo)
            .OrderBy(x => x.Row)
            .AsNoTracking()
            .Select(x => new GetBalanceSheetDetailResultDto
            {
                Order = x.Row,
                CodalRow = x.CodalRow,
                Category = x.CodalCategory,
                Value = x.Value
            });
        return spec;
    }
}