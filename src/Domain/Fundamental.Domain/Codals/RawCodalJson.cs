using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;

namespace Fundamental.Domain.Codals;

/// <summary>
/// Entity for storing raw CODAL JSON data for auditing, traceability and caching.
/// Each TraceNo uniquely identifies a CODAL statement JSON.
/// </summary>
public sealed class RawCodalJson : BaseEntity<Guid>
{
    public RawCodalJson(
        Guid id,
        ulong traceNo,
        DateTime publishDate,
        ReportingType reportingType,
        LetterType statementLetterType,
        Uri? htmlUrl,
        long publisherId,
        string? isin,
        string rawJson,
        DateTime createdAt)
    {
        Id = id;
        TraceNo = traceNo;
        PublishDate = publishDate;
        ReportingType = reportingType;
        StatementLetterType = statementLetterType;
        HtmlUrl = htmlUrl;
        PublisherId = publisherId;
        Isin = isin;
        RawJson = rawJson;
        CreatedAt = createdAt;
    }

    private RawCodalJson()
    {
    }

    /// <summary>
    /// Unique trace number from CODAL API. This is the primary identifier for the JSON.
    /// </summary>
    public ulong TraceNo { get; private set; }

    /// <summary>
    /// Publish date of the report from CODAL.
    /// </summary>
    public DateTime PublishDate { get; private set; }

    /// <summary>
    /// Type of reporting (e.g., Production, Investment).
    /// </summary>
    public ReportingType ReportingType { get; private set; }

    /// <summary>
    /// Letter type from CODAL (e.g., MonthlyActivity, FinancialStatement).
    /// </summary>
    public LetterType StatementLetterType { get; private set; }

    /// <summary>
    /// URL to the HTML version of the statement on CODAL.
    /// </summary>
    public Uri? HtmlUrl { get; private set; }

    /// <summary>
    /// Publisher ID from CODAL (company identifier in their system).
    /// </summary>
    public long PublisherId { get; private set; }

    /// <summary>
    /// ISIN of the symbol, if available.
    /// </summary>
    public string? Isin { get; private set; }

    /// <summary>
    /// Raw JSON data from the API response, stored as jsonb in PostgreSQL.
    /// </summary>
    public string RawJson { get; private set; } = string.Empty;

    /// <summary>
    /// Updates the raw JSON and metadata. Used when a newer version of the same statement is received.
    /// </summary>
    public void Update(
        DateTime publishDate,
        string rawJson,
        string? isin,
        DateTime updatedAt)
    {
        PublishDate = publishDate;
        RawJson = rawJson;
        Isin = isin;
        UpdatedAt = updatedAt;
    }
}
