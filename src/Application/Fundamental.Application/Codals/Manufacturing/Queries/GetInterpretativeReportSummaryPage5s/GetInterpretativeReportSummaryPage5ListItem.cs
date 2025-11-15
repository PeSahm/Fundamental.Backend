namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

public sealed record GetInterpretativeReportSummaryPage5ListItem(
    Guid Id,
    string Isin,
    string Symbol,
    string Title,
    string Uri,
    string Version,
    int FiscalYear,
    int YearEndMonth,
    int ReportMonth,
    ulong TraceNo,
    DateTime? PublishDate
);
