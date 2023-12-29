using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class BalanceSheetDetailSpec : Specification<BalanceSheet, GetBalanceSheetDetailResultDto>
{
    public static BalanceSheetDetailSpec Where(ulong traceNo, uint fiscalYear, uint reportMonth)
    {
        BalanceSheetDetailSpec spec = new();
        spec.Query
            .Select(x => new GetBalanceSheetDetailResultDto
            {
                Order = x.Row,
                CodalRow = x.CodalRow,
                Category = x.CodalCategory,
                Value = (CodalMoney)x.Value,
            })
            .Where(x => x.FiscalYear.Year == fiscalYear)
            .Where(x => x.ReportMonth.Month == reportMonth)
            .Where(x => x.TraceNo == traceNo)
            .OrderBy(x => x.Row)
            .AsNoTracking();
        return spec;
    }
}