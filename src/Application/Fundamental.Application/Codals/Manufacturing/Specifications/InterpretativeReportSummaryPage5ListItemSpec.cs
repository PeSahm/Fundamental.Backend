using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class
    InterpretativeReportSummaryPage5ListItemSpec : Specification<CanonicalInterpretativeReportSummaryPage5,
    GetInterpretativeReportSummaryPage5ListItem>
{
    public InterpretativeReportSummaryPage5ListItemSpec()
    {
        Query
            .AsNoTracking()
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .Select(x => new GetInterpretativeReportSummaryPage5ListItem
            {
                Id = x.Id,
                Isin = x.Symbol.Isin,
                Symbol = x.Symbol.Name,
                Title = x.Symbol.Title,
                Uri = x.Uri,
                Version = x.Version,
                FiscalYear = x.FiscalYear.Year,
                YearEndMonth = x.YearEndMonth.Month,
                ReportMonth = x.ReportMonth.Month,
                TraceNo = x.TraceNo,
                PublishDate = x.PublishDate
            });
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