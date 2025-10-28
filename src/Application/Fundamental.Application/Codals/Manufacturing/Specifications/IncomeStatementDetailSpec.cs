using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementDetails;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public class IncomeStatementDetailSpec : Specification<IncomeStatementDetail, GetIncomeStatementDetailsResultDto>
{
    public static IncomeStatementDetailSpec Where(ulong traceNo, uint fiscalYear, uint reportMonth)
    {
        IncomeStatementDetailSpec spec = new();
        spec.Query.Where(x => x.IncomeStatement.FiscalYear.Year == fiscalYear)
            .Where(x => x.IncomeStatement.ReportMonth.Month == reportMonth)
            .Where(x => x.IncomeStatement.TraceNo == traceNo)
            .OrderBy(x => x.Row)
            .AsNoTracking()
            .Select(x => new GetIncomeStatementDetailsResultDto
            {
                Order = x.Row,
                CodalRow = x.CodalRow,
                Value = x.Value
            });
        return spec;
    }
}