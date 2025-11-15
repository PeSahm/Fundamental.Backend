using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class InterpretativeReportSummaryPage5ListItemSpec : Specification<CanonicalInterpretativeReportSummaryPage5, GetInterpretativeReportSummaryPage5ListItem>
{
    public InterpretativeReportSummaryPage5ListItemSpec()
    {
        Query
            .AsNoTracking()
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .Select(x => new GetInterpretativeReportSummaryPage5ListItem(
                x.Id,
                x.Symbol.Isin,
                x.Symbol.Name,
                x.Symbol.Title,
                x.Uri,
                x.Version,
                x.FiscalYear.Year,
                x.YearEndMonth.Month,
                x.ReportMonth.Month,
                x.TraceNo,
                x.PublishDate
            ));
    }

    public InterpretativeReportSummaryPage5ListItemSpec WhereIsin(string? isin)
    {
        if (!string.IsNullOrWhiteSpace(isin))
        {
            Query.Where(x => x.Symbol.Isin == isin);
        }
        return this;
    }

    public InterpretativeReportSummaryPage5ListItemSpec WhereFiscalYear(int? fiscalYear)
    {
        if (fiscalYear.HasValue)
        {
            Query.Where(x => x.FiscalYear.Year == fiscalYear.Value);
        }
        return this;
    }

    public InterpretativeReportSummaryPage5ListItemSpec WhereReportMonth(int? reportMonth)
    {
        if (reportMonth.HasValue)
        {
            Query.Where(x => x.ReportMonth.Month == reportMonth.Value);
        }
        return this;
    }
}
