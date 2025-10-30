using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Canonical model for Monthly Activity data, normalized from various JSON versions.
/// Uses V5 as the reference structure, mapping to meaningful properties based on captions.
/// Persian date info in captions is ignored as it relates to fiscal year periods.
/// </summary>
public class CanonicalMonthlyActivity : BaseEntity<Guid>
{
    /// <summary>
    /// Version of the Monthly Activity data (e.g., "4" for V5).
    /// </summary>
    public string Version { get; set; }

    /// <summary>
    /// Navigation property to the associated symbol.
    /// </summary>
    public Symbol Symbol { get; set; }

    /// <summary>
    /// Trace number from the CODAL API.
    /// </summary>
    public ulong TraceNo { get; set; }

    /// <summary>
    /// URI of the original report.
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// Fiscal year of the report.
    /// </summary>
    public FiscalYear FiscalYear { get; set; }

    /// <summary>
    /// Currency used in the report.
    /// </summary>
    public IsoCurrency Currency { get; set; } = IsoCurrency.IRR;

    /// <summary>
    /// Year-end month of the fiscal year.
    /// </summary>
    public StatementMonth YearEndMonth { get; set; }

    /// <summary>
    /// Report month.
    /// </summary>
    public StatementMonth ReportMonth { get; set; }

    /// <summary>
    /// Whether the company has sub-company sales.
    /// </summary>
    public bool HasSubCompanySale { get; set; }

    /// <summary>
    /// Collection of buy raw material items.
    /// </summary>
    public ICollection<BuyRawMaterialItem> BuyRawMaterialItems { get; set; } = new List<BuyRawMaterialItem>();

    /// <summary>
    /// Collection of production and sales items.
    /// </summary>
    public ICollection<ProductionAndSalesItem> ProductionAndSalesItems { get; set; } = new List<ProductionAndSalesItem>();

    /// <summary>
    /// Collection of energy consumption items.
    /// </summary>
    public ICollection<EnergyItem> EnergyItems { get; set; } = new List<EnergyItem>();

    /// <summary>
    /// Collection of currency exchange items for export sales.
    /// </summary>
    public ICollection<CurrencyExchangeItem> CurrencyExchangeItems { get; set; } = new List<CurrencyExchangeItem>();

    /// <summary>
    /// Collection of descriptive text items.
    /// </summary>
    public ICollection<MonthlyActivityDescription> Descriptions { get; set; } = new List<MonthlyActivityDescription>();
}