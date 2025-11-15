using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents a miscellaneous expense item (اقلام متفرقه).
/// Maps to interpretativeReportSummaryPage5.nonOperationIncomeAndExpensesMiscellaneousItems.rowItems in V2 JSON.
/// </summary>
public class MiscellaneousExpenseItem
{
    /// <summary>
    /// Row code indicating the type of row.
    /// Data = -1 (custom miscellaneous expense item)
    /// Total = 25 (جمع)
    /// </summary>
    public MiscellaneousExpenseRowCode RowCode { get; set; }

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
    /// Description of the miscellaneous expense item.
    /// Persian: عنوان قلم
    /// Maps to: value_3531 (description column)
    /// Examples: "سایر", "مالیاتهای حقوق و تکلیفی سنوات قبل"
    /// </summary>
    public string? ItemDescription { get; set; }

    /// <summary>
    /// Amount for previous fiscal year period.
    /// Persian: سال مالی منتهی به [date]
    /// Maps to: value_3532 (columnId: 3532)
    /// Typically represents full fiscal year (12 months).
    /// </summary>
    public decimal? PreviousPeriodAmount { get; set; }

    /// <summary>
    /// Amount for current reporting period.
    /// Persian: دوره [X] ماهه منتهی به [date]
    /// Maps to: value_3533 (columnId: 3533)
    /// Typically represents 6-month period.
    /// </summary>
    public decimal? CurrentPeriodAmount { get; set; }

    /// <summary>
    /// Estimated amount for future period.
    /// Persian: برآورد دوره [X] ماهه منتهی به [date]
    /// Maps to: value_3534 (columnId: 3534)
    /// Typically represents estimated 6-month period.
    /// </summary>
    public decimal? EstimatedPeriodAmount { get; set; }

    /// <summary>
    /// Indicates if this is a data row (custom miscellaneous expense item).
    /// </summary>
    public bool IsDataRow => RowCode == MiscellaneousExpenseRowCode.Data;

    /// <summary>
    /// Indicates if this is the total sum row.
    /// </summary>
    public bool IsTotalRow => RowCode == MiscellaneousExpenseRowCode.Total;

    /// <summary>
    /// Gets all data rows (individual miscellaneous expense items) from a collection.
    /// </summary>
    /// <param name="items">Collection of miscellaneous expense items.</param>
    /// <returns>List of data rows only.</returns>
    public static List<MiscellaneousExpenseItem> GetDataRows(IEnumerable<MiscellaneousExpenseItem> items)
    {
        return items.Where(x => x.IsDataRow).ToList();
    }

    /// <summary>
    /// Gets the total sum row from a collection.
    /// </summary>
    /// <param name="items">Collection of miscellaneous expense items.</param>
    /// <returns>Total row if exists, null otherwise.</returns>
    public static MiscellaneousExpenseItem? GetTotal(IEnumerable<MiscellaneousExpenseItem> items)
    {
        return items.FirstOrDefault(x => x.IsTotalRow);
    }
}
