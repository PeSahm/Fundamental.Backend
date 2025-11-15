namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

/// <summary>
/// List item for Interpretative Report Summary Page 5 queries.
/// Contains basic metadata without detailed collections.
/// </summary>
public sealed class GetInterpretativeReportSummaryPage5ListItem
{
    public Guid Id { get; init; }
    public string Isin { get; init; } = null!;
    public string Symbol { get; init; } = null!;
    public string Title { get; init; } = null!;
    public string Uri { get; init; } = null!;
    public string Version { get; init; } = null!;
    public int FiscalYear { get; init; }
    public int YearEndMonth { get; init; }
    public int ReportMonth { get; init; }
    public ulong TraceNo { get; init; }
    public DateTime? PublishDate { get; init; }
}
