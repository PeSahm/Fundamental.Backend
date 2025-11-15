using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents an item in other non-operating expenses section (سایر هزینه‌های غیرعملیاتی).
/// Maps to interpretativeReportSummaryPage5.otherNonOperatingExpenses.rowItems in V2 JSON.
/// </summary>
public class OtherNonOperatingExpenseItem
{
    /// <summary>
    /// Row code indicating the type of row.
    /// Data = -1 (custom expense item)
    /// Total = 7 (جمع)
    /// </summary>
    public OtherNonOperatingExpenseRowCode RowCode { get; set; }

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
    /// Description of the expense item.
    /// Persian: عنوان قلم
    /// Maps to: value_3521 (description column)
    /// Examples: "سایر", "سود(زیان) ناشی از تسعیر دارائیها و بدهی‏های ارزی عملیاتی"
    /// </summary>
    public string? ItemDescription { get; set; }

    /// <summary>
    /// Amount for previous fiscal year period.
    /// Persian: سال مالی منتهی به [date]
    /// Maps to: value_3522 (columnId: 3522)
    /// Typically represents full fiscal year (12 months).
    /// </summary>
    public decimal? PreviousPeriodAmount { get; set; }

    /// <summary>
    /// Amount for current reporting period.
    /// Persian: دوره [X] ماهه منتهی به [date]
    /// Maps to: value_3523 (columnId: 3523)
    /// Typically represents 6-month period.
    /// </summary>
    public decimal? CurrentPeriodAmount { get; set; }

    /// <summary>
    /// Estimated amount for future period.
    /// Persian: برآورد دوره [X] ماهه منتهی به [date]
    /// Maps to: value_3524 (columnId: 3524)
    /// Typically represents estimated 6-month period.
    /// </summary>
    public decimal? EstimatedPeriodAmount { get; set; }

    /// <summary>
    /// Indicates if this is a data row (custom expense item).
    /// </summary>
    public bool IsDataRow => RowCode == OtherNonOperatingExpenseRowCode.Data;

    /// <summary>
    /// Indicates if this is the total sum row.
    /// </summary>
    public bool IsTotalRow => RowCode == OtherNonOperatingExpenseRowCode.Total;

    /// <summary>
    /// Gets all data rows (individual expense items) from a collection.
    /// </summary>
    /// <param name="items">Collection of expense items.</param>
    /// <returns>List of data rows only.</returns>
    public static List<OtherNonOperatingExpenseItem> GetDataRows(IEnumerable<OtherNonOperatingExpenseItem> items)
    {
        return items.Where(x => x.IsDataRow).ToList();
    }

    /// <summary>
    /// Gets the total sum row from a collection.
    /// </summary>
    /// <param name="items">Collection of expense items.</param>
    /// <returns>Total row if exists, null otherwise.</returns>
    public static OtherNonOperatingExpenseItem? GetTotal(IEnumerable<OtherNonOperatingExpenseItem> items)
    {
        return items.FirstOrDefault(x => x.IsTotalRow);
    }
}
