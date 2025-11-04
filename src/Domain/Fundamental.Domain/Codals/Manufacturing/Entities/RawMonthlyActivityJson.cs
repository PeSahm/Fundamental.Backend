using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Entity for storing raw Monthly Activity JSON data for auditing and traceability.
/// Stored alongside the canonical model in PostgreSQL with jsonb column.
/// </summary>
public class RawMonthlyActivityJson : BaseEntity<Guid>
{
    public RawMonthlyActivityJson(
        Guid id,
        long traceNo,
        Symbol symbol,
        DateTime publishDate,
        CodalVersion version,
        string rawJson,
        DateTime createdAt)
    {
        Id = id;
        TraceNo = traceNo;
        Symbol = symbol;
        PublishDate = publishDate;
        Version = version;
        RawJson = rawJson;
        CreatedAt = createdAt;
    }

    protected RawMonthlyActivityJson()
    {
    }

    /// <summary>
    /// Trace number from the CODAL API.
    /// </summary>
    public long TraceNo { get; private set; }

    /// <summary>
    /// Symbol identifier.
    /// </summary>
    public Symbol Symbol { get; private set; } = null!;

    /// <summary>
    /// Publish date of the report.
    /// </summary>
    public DateTime PublishDate { get; private set; }

    /// <summary>
    /// Version of the Monthly Activity data.
    /// </summary>
    public CodalVersion Version { get; private set; }

    /// <summary>
    /// Raw JSON data from the API response, stored as jsonb in PostgreSQL.
    /// </summary>
    public string RawJson { get; private set; }

    public void Update(long traceNo, DateTime publishDate, string rawJson, DateTime updatedAt)
    {
        TraceNo = traceNo;
        PublishDate = publishDate;
        RawJson = rawJson;
        UpdatedAt = updatedAt;
    }
}