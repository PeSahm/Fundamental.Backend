using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents an investment income item (درآمدهای سرمایه‌گذاری).
/// Maps to interpretativeReportSummaryPage5.nonOperationIncomeAndExpensesInvestmentIncome.rowItems in V2 JSON.
/// </summary>
public class InvestmentIncomeItem
{
    /// <summary>
    /// Row code indicating the type of row.
    /// Data = -1 (custom investment income item)
    /// Total = 22 (جمع)
    /// </summary>
    public InvestmentIncomeRowCode RowCode { get; set; }

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
    /// Description of the investment income item.
    /// Persian: عنوان قلم
    /// Maps to: value_2451 (description column)
    /// Examples: "سود حاصل از سپرده‏های‏ بانکی", "سود سهام حاصل از سرمایه‏ گذاری‏ه ای بلندمدت"
    /// </summary>
    public string? ItemDescription { get; set; }

    /// <summary>
    /// Amount for previous fiscal year period.
    /// Persian: سال مالی منتهی به [date]
    /// Maps to: value_2452 (columnId: 2452)
    /// Typically represents full fiscal year (12 months).
    /// </summary>
    public decimal? PreviousPeriodAmount { get; set; }

    /// <summary>
    /// Amount for current reporting period.
    /// Persian: دوره [X] ماهه منتهی به [date]
    /// Maps to: value_2453 (columnId: 2453)
    /// Typically represents 6-month period.
    /// </summary>
    public decimal? CurrentPeriodAmount { get; set; }

    /// <summary>
    /// Estimated amount for future period.
    /// Persian: برآورد دوره [X] ماهه منتهی به [date]
    /// Maps to: value_2454 (columnId: 2454)
    /// Typically represents estimated 6-month period.
    /// </summary>
    public decimal? EstimatedPeriodAmount { get; set; }

    /// <summary>
    /// Indicates if this is a data row (custom investment income item).
    /// </summary>
    public bool IsDataRow => RowCode == InvestmentIncomeRowCode.Data;

    /// <summary>
    /// Indicates if this is the total sum row.
    /// </summary>
    public bool IsTotalRow => RowCode == InvestmentIncomeRowCode.Total;

    /// <summary>
    /// Gets all data rows (individual investment income items) from a collection.
    /// </summary>
    /// <param name="items">Collection of investment income items.</param>
    /// <returns>List of data rows only.</returns>
    public static List<InvestmentIncomeItem> GetDataRows(IEnumerable<InvestmentIncomeItem> items)
    {
        return items.Where(x => x.IsDataRow).ToList();
    }

    /// <summary>
    /// Gets the total sum row from a collection.
    /// </summary>
    /// <param name="items">Collection of investment income items.</param>
    /// <returns>Total row if exists, null otherwise.</returns>
    public static InvestmentIncomeItem? GetTotal(IEnumerable<InvestmentIncomeItem> items)
    {
        return items.FirstOrDefault(x => x.IsTotalRow);
    }
}
