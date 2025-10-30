using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Entity for storing raw Monthly Activity JSON data for auditing and traceability.
/// Stored alongside the canonical model in PostgreSQL with jsonb column.
/// </summary>
public class RawMonthlyActivityJson : BaseEntity<Guid>
{
    /// <summary>
    /// Trace number from the CODAL API.
    /// </summary>
    public long TraceNo { get; set; }

    /// <summary>
    /// Symbol identifier.
    /// </summary>
    public Symbol Symbol { get; set; } = null!;

    /// <summary>
    /// Publish date of the report.
    /// </summary>
    public DateTime PublishDate { get; set; }

    /// <summary>
    /// Version of the Monthly Activity data (e.g., "1", "2", etc.).
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Raw JSON data from the API response, stored as jsonb in PostgreSQL.
    /// </summary>
    public string RawJson { get; set; }
}