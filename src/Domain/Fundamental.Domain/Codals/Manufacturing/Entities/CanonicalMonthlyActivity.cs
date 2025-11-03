using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Canonical model for Monthly Activity data, normalized from various JSON versions.
/// Uses V5 as the reference structure, mapping to meaningful properties based on captions.
/// Persian date info in captions is ignored as it relates to fiscal year periods.
/// This entity represents a complete monthly activity report for a specific symbol and period.
/// </summary>
public class CanonicalMonthlyActivity : BaseEntity<Guid>
{
    public CanonicalMonthlyActivity(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        DateTime publishDate,
        string version
    )
    {
        Id = id;
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        PublishDate = publishDate;
        Version = version;
        BuyRawMaterialItems = new List<BuyRawMaterialItem>();
        ProductionAndSalesItems = new List<ProductionAndSalesItem>();
        EnergyItems = new List<EnergyItem>();
        CurrencyExchangeItems = new List<CurrencyExchangeItem>();
        Descriptions = new List<MonthlyActivityDescription>();
        CreatedAt = DateTime.Now.ToUniversalTime();
    }

    protected CanonicalMonthlyActivity()
    {
    }

    /// <summary>
    /// Version of the Monthly Activity data (e.g., "5" for V5, "4" for V4, etc.).
    /// Indicates which CODAL format version this data was extracted from.
    /// </summary>
    public string Version { get; private set; }

    /// <summary>
    /// Publish date of the monthly activity report (Gregorian).
    /// Sourced from CODAL statement's PublishDateMiladi.
    /// </summary>
    public DateTime? PublishDate { get; set; }

    /// <summary>
    /// Navigation property to the associated symbol.
    /// The company/entity this monthly activity report belongs to.
    /// </summary>
    public Symbol Symbol { get; set; }

    /// <summary>
    /// Trace number from the CODAL API.
    /// Unique identifier for this specific report instance.
    /// </summary>
    public ulong TraceNo { get; set; }

    /// <summary>
    /// URI of the original report.
    /// Link to the source CODAL document for reference.
    /// </summary>
    public string Uri { get; set; }

    /// <summary>
    /// Fiscal year of the report.
    /// The Persian calendar year this report covers (e.g., 1404).
    /// </summary>
    public FiscalYear FiscalYear { get; set; }

    /// <summary>
    /// Currency used in the report.
    /// Typically IRR (Iranian Rial) for domestic reports.
    /// </summary>
    public IsoCurrency Currency { get; set; } = IsoCurrency.IRR;

    /// <summary>
    /// Year-end month of the fiscal year.
    /// Usually 12 for standard Persian calendar fiscal years.
    /// </summary>
    public StatementMonth YearEndMonth { get; set; }

    /// <summary>
    /// Report month.
    /// The month this monthly activity report covers (1-12).
    /// </summary>
    public StatementMonth ReportMonth { get; set; }

    /// <summary>
    /// Whether the company has sub-company sales.
    /// Indicates if the company reports sales from subsidiaries.
    /// </summary>
    public bool HasSubCompanySale { get; set; }

    /// <summary>
    /// Collection of raw material purchase items.
    /// Raw materials purchased during the reporting period including domestic and imported materials.
    /// Maps to V5 buyRawMaterial section with columnIds starting with 346xx.
    /// Includes year-to-date, corrected, monthly, cumulative, and previous year purchase data.
    /// </summary>
    public ICollection<BuyRawMaterialItem> BuyRawMaterialItems { get; set; } = new List<BuyRawMaterialItem>();

    /// <summary>
    /// Collection of production and sales items.
    /// Products manufactured and sold during the reporting period including internal and export sales.
    /// Maps to V5 productionAndSales section with columnIds starting with 119xx.
    /// Includes both data rows (product details) and summary rows (totals by category).
    /// Use helper methods to retrieve specific categories (internal, export, service, etc.).
    /// </summary>
    public ICollection<ProductionAndSalesItem> ProductionAndSalesItems { get; set; } = new List<ProductionAndSalesItem>();

    /// <summary>
    /// Collection of energy consumption items.
    /// Energy types (electricity, gas, etc.) used in production processes with consumption amounts and costs.
    /// Maps to V5 energy section with columnIds starting with 319xx.
    /// Includes year-to-date, corrected, monthly, cumulative, previous year, and forecast data.
    /// </summary>
    public ICollection<EnergyItem> EnergyItems { get; set; } = new List<EnergyItem>();

    /// <summary>
    /// Collection of currency exchange items for foreign currency sources and uses.
    /// Foreign currency transactions related to exports, imports, loans, and other international activities.
    /// Maps to V5 sourceUsesCurrency section with columnIds starting with 364xx.
    /// Includes year-to-date, corrected, monthly, cumulative, previous year, and forecast data.
    /// </summary>
    public ICollection<CurrencyExchangeItem> CurrencyExchangeItems { get; set; } = new List<CurrencyExchangeItem>();

    // ========== Helper Methods for Querying Currency Exchange Data ==========

    /// <summary>
    /// Gets all currency sources data rows (Category=Sources, RowCode=Data).
    /// دریافت تمام ردیف‌های داده منابع ارزی (فروش صادراتی، وام خارجی و غیره).
    /// </summary>
    public IEnumerable<CurrencyExchangeItem> GetCurrencySourcesDataRows()
        => CurrencyExchangeItems.Where(x => x.IsDataRow && x.IsSourcesRow);

    /// <summary>
    /// Gets all currency uses data rows (Category=Uses, RowCode=Data).
    /// دریافت تمام ردیف‌های داده مصارف ارزی (خرید وارداتی، پرداخت‌ها و غیره).
    /// </summary>
    public IEnumerable<CurrencyExchangeItem> GetCurrencyUsesDataRows()
        => CurrencyExchangeItems.Where(x => x.IsDataRow && x.IsUsesRow);

    /// <summary>
    /// Gets the total sources sum row (RowCode=SourcesSum).
    /// دریافت ردیف جمع کل منابع ارزی.
    /// </summary>
    public CurrencyExchangeItem? GetCurrencySourcesSum()
        => CurrencyExchangeItems.FirstOrDefault(x => x.RowCode == CurrencyExchangeRowCode.SourcesSum);

    /// <summary>
    /// Gets the total uses sum row (RowCode=UsesSum).
    /// دریافت ردیف جمع کل مصارف ارزی.
    /// </summary>
    public CurrencyExchangeItem? GetCurrencyUsesSum()
        => CurrencyExchangeItems.FirstOrDefault(x => x.RowCode == CurrencyExchangeRowCode.UsesSum);

    /// <summary>
    /// Gets all data rows (sources and uses combined).
    /// دریافت تمام ردیف‌های داده (منابع و مصارف).
    /// </summary>
    public IEnumerable<CurrencyExchangeItem> GetCurrencyExchangeDataRows()
        => CurrencyExchangeItems.Where(x => x.IsDataRow);

    /// <summary>
    /// Gets all summary rows (all totals and sums).
    /// دریافت تمام ردیف‌های جمع‌بندی.
    /// </summary>
    public IEnumerable<CurrencyExchangeItem> GetAllCurrencyExchangeSummaryRows()
        => CurrencyExchangeItems.Where(x => x.IsSummaryRow);

    /// <summary>
    /// Collection of descriptive text items.
    /// Additional textual information, explanations, and notes about the monthly activity report.
    /// Maps to V5 productMonthlyActivityDesc1 section.
    /// Contains free-form text descriptions with optional categorization.
    /// </summary>
    public ICollection<MonthlyActivityDescription> Descriptions { get; set; } = new List<MonthlyActivityDescription>();

    // ========== Helper Methods for Querying Energy Data ==========

    /// <summary>
    /// Gets all energy data rows (actual consumption entries).
    /// Returns items with RowCode = Data, excluding summary rows.
    /// ردیف‌های داده مصرف انرژی.
    /// </summary>
    public IEnumerable<EnergyItem> GetEnergyDataRows()
    {
        return EnergyItems
            .Where(x => x.IsDataRow)
            .ToList();
    }

    /// <summary>
    /// Gets the total energy consumption sum row.
    /// This row has RowCode = TotalSum and contains the sum of all energy consumption.
    /// جمع کل مصرف انرژی.
    /// </summary>
    public EnergyItem? GetEnergyTotalSum()
    {
        return EnergyItems
            .FirstOrDefault(x => x.RowCode == EnergyRowCode.TotalSum);
    }

    /// <summary>
    /// Gets all summary rows for energy consumption.
    /// These have RowCode != Data and contain aggregated data.
    /// </summary>
    public IEnumerable<EnergyItem> GetAllEnergySummaryRows()
    {
        return EnergyItems
            .Where(x => x.IsSummaryRow)
            .ToList();
    }

    // ========== Helper Methods for Querying Buy Raw Material Data ==========

    /// <summary>
    /// Gets all domestic (internal) raw material data rows.
    /// Returns only data rows (RowCode = Data) with Category = Domestic.
    /// Excludes summary rows. Represents materials purchased within Iran.
    /// خرید مواد اولیه داخلی.
    /// </summary>
    public IEnumerable<BuyRawMaterialItem> GetDomesticRawMaterials()
    {
        return BuyRawMaterialItems
            .Where(x => x.RowCode == BuyRawMaterialRowCode.Data &&
                        x.Category == BuyRawMaterialCategory.Domestic)
            .ToList();
    }

    /// <summary>
    /// Gets all imported raw material items (including both data and summary rows).
    /// Returns all items with Category = Imported.
    /// Represents materials purchased from abroad.
    /// خرید مواد اولیه خارجی (وارداتی).
    /// </summary>
    public IEnumerable<BuyRawMaterialItem> GetImportedRawMaterials()
    {
        return BuyRawMaterialItems
            .Where(x => x.Category == BuyRawMaterialCategory.Imported)
            .ToList();
    }

    /// <summary>
    /// Gets all total raw material items (including both data and summary rows).
    /// Returns all items with Category = Total.
    /// Contains the aggregate of all material purchases.
    /// جمع کل خرید مواد اولیه.
    /// </summary>
    public IEnumerable<BuyRawMaterialItem> GetTotalRawMaterials()
    {
        return BuyRawMaterialItems
            .Where(x => x.Category == BuyRawMaterialCategory.Total)
            .ToList();
    }

    /// <summary>
    /// Gets all raw material data rows (excluding summary rows).
    /// Returns items that have RowCode = Data, representing actual material entries.
    /// </summary>
    public IEnumerable<BuyRawMaterialItem> GetRawMaterialDataRows()
    {
        return BuyRawMaterialItems
            .Where(x => x.IsDataRow)
            .ToList();
    }

    /// <summary>
    /// Gets the domestic raw materials sum row.
    /// This row has RowCode = DomesticSum and contains the sum of all domestic purchases.
    /// جمع مواد اولیه داخلی.
    /// </summary>
    public BuyRawMaterialItem? GetDomesticRawMaterialsSum()
    {
        return BuyRawMaterialItems
            .FirstOrDefault(x => x.RowCode == BuyRawMaterialRowCode.DomesticSum);
    }

    /// <summary>
    /// Gets the imported raw materials sum row.
    /// This row has RowCode = ImportedSum and contains the sum of all imported purchases.
    /// جمع مواد اولیه وارداتی.
    /// </summary>
    public BuyRawMaterialItem? GetImportedRawMaterialsSum()
    {
        return BuyRawMaterialItems
            .FirstOrDefault(x => x.RowCode == BuyRawMaterialRowCode.ImportedSum);
    }

    /// <summary>
    /// Gets the total raw materials sum row (grand total).
    /// This row has RowCode = TotalSum and contains the sum of all raw material purchases.
    /// جمع کل مواد اولیه.
    /// </summary>
    public BuyRawMaterialItem? GetRawMaterialsTotalSum()
    {
        return BuyRawMaterialItems
            .FirstOrDefault(x => x.RowCode == BuyRawMaterialRowCode.TotalSum);
    }

    /// <summary>
    /// Gets all summary rows for raw materials.
    /// These have RowCode != Data and contain aggregated data.
    /// </summary>
    public IEnumerable<BuyRawMaterialItem> GetAllRawMaterialSummaryRows()
    {
        return BuyRawMaterialItems
            .Where(x => x.IsSummaryRow)
            .ToList();
    }

    // ========== Helper Methods for Querying Production and Sales Data ==========

    /// <summary>
    /// Gets all data rows (product details) from production and sales items.
    /// These have RowCode = -1 and contain actual product data.
    /// </summary>
    public IEnumerable<ProductionAndSalesItem> GetProductionAndSalesDataRows()
    {
        return ProductionAndSalesItems
            .Where(x => x.RowCode == ProductionSalesRowCode.Data)
            .ToList();
    }

    /// <summary>
    /// Gets all internal (domestic) sale data rows.
    /// These have RowCode = -1 and Category = 1.
    /// </summary>
    public IEnumerable<ProductionAndSalesItem> GetInternalSaleDataRows()
    {
        return ProductionAndSalesItems
            .Where(x => x.RowCode == ProductionSalesRowCode.Data &&
                        x.Category == ProductionSalesCategory.Internal)
            .ToList();
    }

    /// <summary>
    /// Gets all export sale data rows.
    /// These have RowCode = -1 and Category = 2.
    /// </summary>
    public IEnumerable<ProductionAndSalesItem> GetExportSaleDataRows()
    {
        return ProductionAndSalesItems
            .Where(x => x.RowCode == ProductionSalesRowCode.Data &&
                        x.Category == ProductionSalesCategory.Export)
            .ToList();
    }

    /// <summary>
    /// Gets the internal sale summary row.
    /// This row has RowCode = 5 and contains the sum of all internal sales.
    /// Corresponds to the blue box in the report (جمع فروش داخلی).
    /// </summary>
    public ProductionAndSalesItem? GetInternalSaleSummary()
    {
        return ProductionAndSalesItems
            .FirstOrDefault(x => x.RowCode == ProductionSalesRowCode.InternalSale &&
                                 x.Category == ProductionSalesCategory.Sum);
    }

    /// <summary>
    /// Gets the export sale summary row.
    /// This row has RowCode = 8 and contains the sum of all export sales.
    /// Corresponds to the red box in the report (جمع فروش صادراتی).
    /// </summary>
    public ProductionAndSalesItem? GetExportSaleSummary()
    {
        return ProductionAndSalesItems
            .FirstOrDefault(x => x.RowCode == ProductionSalesRowCode.ExportSale &&
                                 x.Category == ProductionSalesCategory.Sum);
    }

    /// <summary>
    /// Gets the total sum row (grand total).
    /// This row has RowCode = 16 and contains the overall sum of all sales.
    /// Corresponds to the green box in the report (جمع).
    /// This is the most important summary row as it represents the complete monthly activity.
    /// </summary>
    public ProductionAndSalesItem? GetTotalSummary()
    {
        return ProductionAndSalesItems
            .FirstOrDefault(x => x.RowCode == ProductionSalesRowCode.TotalSum &&
                                 x.Category == ProductionSalesCategory.Sum);
    }

    /// <summary>
    /// Gets the service income summary row.
    /// This row has RowCode = 11 and contains the sum of service revenues.
    /// </summary>
    public ProductionAndSalesItem? GetServiceIncomeSummary()
    {
        return ProductionAndSalesItems
            .FirstOrDefault(x => x.RowCode == ProductionSalesRowCode.ServiceIncome &&
                                 x.Category == ProductionSalesCategory.Sum);
    }

    /// <summary>
    /// Gets the return sale summary row.
    /// This row has RowCode = 14 and contains the sum of returned sales.
    /// </summary>
    public ProductionAndSalesItem? GetReturnSaleSummary()
    {
        return ProductionAndSalesItems
            .FirstOrDefault(x => x.RowCode == ProductionSalesRowCode.ReturnSale &&
                                 x.Category == ProductionSalesCategory.Sum);
    }

    /// <summary>
    /// Gets the discount summary row.
    /// This row has RowCode = 15 and contains the sum of discounts.
    /// </summary>
    public ProductionAndSalesItem? GetDiscountSummary()
    {
        return ProductionAndSalesItems
            .FirstOrDefault(x => x.RowCode == ProductionSalesRowCode.Discount &&
                                 x.Category == ProductionSalesCategory.Discount);
    }

    /// <summary>
    /// Gets all summary rows.
    /// These have RowCode >= 0 and contain aggregated data.
    /// </summary>
    public IEnumerable<ProductionAndSalesItem> GetAllSummaryRows()
    {
        return ProductionAndSalesItems
            .Where(x => x.IsSummaryRow)
            .ToList();
    }
}