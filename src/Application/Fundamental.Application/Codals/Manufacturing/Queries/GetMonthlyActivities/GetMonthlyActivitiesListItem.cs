namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;

/// <summary>
/// List item for Monthly Activity queries.
/// Contains basic metadata without detailed collections.
/// </summary>
public sealed class GetMonthlyActivitiesListItem
{
    /// <summary>
    /// Unique identifier of the monthly activity report.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// ISIN code of the symbol.
    /// </summary>
    public string Isin { get; init; }

    /// <summary>
    /// Symbol name.
    /// </summary>
    public string Symbol { get; init; }

    /// <summary>
    /// Full title of the company.
    /// </summary>
    public string Title { get; init; }

    /// <summary>
    /// URI of the original CODAL report.
    /// </summary>
    public string Uri { get; init; }

    /// <summary>
    /// Version of the Monthly Activity data format (e.g., "5" for V5).
    /// </summary>
    public string Version { get; init; }

    /// <summary>
    /// Fiscal year of the report.
    /// </summary>
    public ushort FiscalYear { get; init; }

    /// <summary>
    /// Year-end month of the fiscal year.
    /// </summary>
    public ushort YearEndMonth { get; init; }

    /// <summary>
    /// Report month (1-12).
    /// </summary>
    public ushort ReportMonth { get; init; }

    /// <summary>
    /// Whether the company has sub-company sales.
    /// </summary>
    public bool HasSubCompanySale { get; init; }

    /// <summary>
    /// Trace number from CODAL API.
    /// </summary>
    public ulong TraceNo { get; init; }

    /// <summary>
    /// Creation timestamp.
    /// </summary>
    public DateTime CreatedAt { get; init; }

    /// <summary>
    /// Last update timestamp.
    /// </summary>
    public required DateTime? UpdatedAt { get; init; }
}