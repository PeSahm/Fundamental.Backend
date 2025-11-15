using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;

/// <summary>
/// Detail result item for Monthly Activity queries based on CanonicalMonthlyActivity entity.
/// Contains basic metadata and collections of detailed items.
/// </summary>
public sealed class GetMonthlyActivityDetailItem
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

    /// <summary>
    /// Collection of production and sales items (products, services, internal/export sales).
    /// </summary>
    public ICollection<ProductionAndSalesItem> ProductionAndSalesItems { get; init; } = new List<ProductionAndSalesItem>();

    /// <summary>
    /// Collection of raw material purchase items (domestic and imported materials).
    /// </summary>
    public ICollection<BuyRawMaterialItem> BuyRawMaterialItems { get; init; } = new List<BuyRawMaterialItem>();

    /// <summary>
    /// Collection of energy consumption items (electricity, gas, etc.).
    /// </summary>
    public ICollection<EnergyItem> EnergyItems { get; init; } = new List<EnergyItem>();

    /// <summary>
    /// Collection of currency exchange items (foreign currency sources and uses).
    /// </summary>
    public ICollection<CurrencyExchangeItem> CurrencyExchangeItems { get; init; } = new List<CurrencyExchangeItem>();

    /// <summary>
    /// Collection of descriptive text items and notes.
    /// </summary>
    public ICollection<MonthlyActivityDescription> Descriptions { get; init; } = new List<MonthlyActivityDescription>();
}