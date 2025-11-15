using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents an item in other operating income section (سایر درآمدهای عملیاتی).
/// Maps to interpretativeReportSummaryPage5.otherOperatingIncome.rowItems in V2 JSON.
/// </summary>
public class OtherOperatingIncomeItem
{
    /// <summary>
    /// Row code indicating the type of row.
    /// Data = -1 (custom income item)
    /// Total = 4 (جمع)
    /// </summary>
    public OtherOperatingIncomeRowCode RowCode { get; set; }

    /// <summary>
    /// Category identifier for the row.
    /// </summary>
    public int Category { get; set; }

    /// <summary>
    /// Type of row: CustomRow or FixedRow.
    /// </summary>
    public InterpretativeReportRowType? RowType { get; set; }

    /// <summary>
    /// Unique identifier for custom rows.
    /// Only present for rowCode = -1 (data rows).
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Description of the income item.
    /// Persian: عنوان قلم
    /// Maps to: value_2421 (description column)
    /// Examples: "سود ناشی از تسعیر ارز", "درآمد حاصل از خدمات"
    /// </summary>
    public string? ItemDescription { get; set; }

    /// <summary>
    /// Amount for previous fiscal year period.
    /// Persian: سال مالی منتهی به [date]
    /// Maps to: value_2422 (columnId: 2422)
    /// Typically represents full fiscal year (12 months).
    /// </summary>
    public decimal? PreviousPeriodAmount { get; set; }

    /// <summary>
    /// Amount for current reporting period.
    /// Persian: دوره [X] ماهه منتهی به [date]
    /// Maps to: value_2423 (columnId: 2423)
    /// Typically represents 6-month period.
    /// </summary>
    public decimal? CurrentPeriodAmount { get; set; }

    /// <summary>
    /// Estimated amount for future period.
    /// Persian: برآورد دوره [X] ماهه منتهی به [date]
    /// Maps to: value_2424 (columnId: 2424)
    /// Typically represents estimated 6-month period.
    /// </summary>
    public decimal? EstimatedPeriodAmount { get; set; }

    /// <summary>
    /// Indicates if this is a data row (custom income item).
    /// </summary>
    public bool IsDataRow => RowCode == OtherOperatingIncomeRowCode.Data;

    /// <summary>
    /// Indicates if this is the total sum row.
    /// </summary>
    public bool IsTotalRow => RowCode == OtherOperatingIncomeRowCode.Total;

    /// <summary>
    /// Gets all data rows (individual income items) from a collection.
    /// </summary>
    /// <param name="items">Collection of income items.</param>
    /// <returns>List of data rows only.</returns>
    public static List<OtherOperatingIncomeItem> GetDataRows(IEnumerable<OtherOperatingIncomeItem> items)
    {
        return items.Where(x => x.IsDataRow).ToList();
    }

    /// <summary>
    /// Gets the total sum row from a collection.
    /// </summary>
    /// <param name="items">Collection of income items.</param>
    /// <returns>Total row if exists, null otherwise.</returns>
    public static OtherOperatingIncomeItem? GetTotal(IEnumerable<OtherOperatingIncomeItem> items)
    {
        return items.FirstOrDefault(x => x.IsTotalRow);
    }
}
