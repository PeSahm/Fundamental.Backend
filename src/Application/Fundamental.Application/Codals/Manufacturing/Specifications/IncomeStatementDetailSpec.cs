using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementDetails;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class IncomeStatementDetailSpec : Specification<IncomeStatement, GetIncomeStatementDetailsResultDto>
{
    public static IncomeStatementDetailSpec Where(ulong traceNo, uint fiscalYear, uint reportMonth)
    {
        IncomeStatementDetailSpec spec = new();
        spec.Query
            .Select(x => new GetIncomeStatementDetailsResultDto
            {
                Order = x.Row,
                CodalRow = x.CodalRow,
                Value = (CodalMoney)x.Value,
            })
            .Where(x => x.FiscalYear.Year == fiscalYear)
            .Where(x => x.ReportMonth.Month == reportMonth)
            .Where(x => x.TraceNo < traceNo)
            .OrderBy(x => x.Row)
            .AsNoTracking();
        return spec;
    }
}