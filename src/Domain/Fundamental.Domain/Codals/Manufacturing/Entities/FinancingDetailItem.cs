using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents financing details for a company.
/// Maps to interpretativeReportSummaryPage5.detailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod.rowItems
/// and detailsOfTheFinancingOfTheCompany-Est1.rowItems in V2 JSON.
/// Contains information about bank facilities, loans, and financial expenses.
/// </summary>
public class FinancingDetailItem
{
    /// <summary>
    /// Row code indicating the type of row.
    /// See FinancingDetailRowCode enum for all possible values.
    /// </summary>
    public FinancingDetailRowCode RowCode { get; set; }

    /// <summary>
    /// Category identifier for the row.
    /// </summary>
    public int Category { get; set; }

    /// <summary>
    /// Type of row: CustomRow or FixedRow.
    /// </summary>
    public InterpretativeReportRowType? RowType { get; set; }

    /// <summary>
    /// Source or description of financing.
    /// Persian: محل تامین
    /// Maps to: value_2431 or value_23371 (columnId: 2431 or 23371)
    /// Examples: "تسهیلات دریافتی از بانکها", "جمع"
    /// </summary>
    public string? FinancingSource { get; set; }

    /// <summary>
    /// Interest rate percentage.
    /// Persian: نرخ سود
    /// Maps to: value_2432 or value_23372 (columnId: 2432 or 23372)
    /// </summary>
    public decimal? InterestRate { get; set; }

    /// <summary>
    /// Beginning balance of facilities (Rial and foreign combined).
    /// Persian: مانده اول دوره تسهیلات ارزی و ریالی (میلیون ریال)
    /// Maps to: value_2433 or value_23373 (columnId: 2433 or 23373)
    /// </summary>
    public decimal? BeginningBalance { get; set; }

    /// <summary>
    /// Ending balance in Rial (principal and interest).
    /// Persian: مانده پایان دوره (اصل و فرع) - ریالی
    /// Maps to: value_2434 or value_23374 (columnId: 2434 or 23374)
    /// </summary>
    public decimal? EndingBalanceRial { get; set; }

    /// <summary>
    /// Type of foreign currency.
    /// Persian: ارزی - نوع ارز
    /// Maps to: value_2435 or value_23375 (columnId: 2435 or 23375)
    /// Examples: "یورو", "دلار"
    /// </summary>
    public string? CurrencyType { get; set; }

    /// <summary>
    /// Amount in foreign currency.
    /// Persian: مبلغ ارزی
    /// Maps to: value_2436 or value_23376 (columnId: 2436 or 23376)
    /// </summary>
    public decimal? ForeignAmount { get; set; }

    /// <summary>
    /// Rial equivalent of foreign currency facilities.
    /// Persian: معادل ریالی تسهیلات ارزی (میلیون ریال)
    /// Maps to: value_2437 or value_23377 (columnId: 2437 or 23377)
    /// </summary>
    public decimal? ForeignRialEquivalent { get; set; }

    /// <summary>
    /// Short-term portion of ending balance.
    /// Persian: مانده پایان دوره به تفکیک سررسید - کوتاه مدت
    /// Maps to: value_2438 or value_23378 (columnId: 2438 or 23378)
    /// </summary>
    public decimal? ShortTerm { get; set; }

    /// <summary>
    /// Long-term portion of ending balance.
    /// Persian: بلند مدت
    /// Maps to: value_2439 or value_23379 (columnId: 2439 or 23379)
    /// </summary>
    public decimal? LongTerm { get; set; }

    /// <summary>
    /// Financial expense for the period.
    /// Persian: مبلغ هزینه مالی طی دوره
    /// Maps to: value_24310 or value_233710 (columnId: 24310 or 233710)
    /// </summary>
    public decimal? FinancialExpense { get; set; }

    /// <summary>
    /// Other descriptions or notes.
    /// Persian: سایر توضیحات
    /// Maps to: value_24311 or value_233711 (columnId: 24311 or 233711)
    /// </summary>
    public string? OtherDescriptions { get; set; }

    /// <summary>
    /// Indicates if this row represents bank facilities received.
    /// </summary>
    public bool IsBankFacilities => RowCode == FinancingDetailRowCode.BankFacilitiesCurrent || RowCode == FinancingDetailRowCode.BankFacilitiesEstimated;

    /// <summary>
    /// Indicates if this row represents facilities settled since beginning of fiscal year.
    /// </summary>
    public bool IsSettledFacilities => RowCode == FinancingDetailRowCode.SettledFacilitiesCurrent || RowCode == FinancingDetailRowCode.SettledFacilitiesEstimated;

    /// <summary>
    /// Indicates if this row represents the total sum.
    /// </summary>
    public bool IsTotal => RowCode == FinancingDetailRowCode.TotalCurrent || RowCode == FinancingDetailRowCode.TotalEstimated;

    /// <summary>
    /// Indicates if this row represents transfer to assets.
    /// </summary>
    public bool IsTransferToAssets => RowCode == FinancingDetailRowCode.TransferToAssetsCurrent || RowCode == FinancingDetailRowCode.TransferToAssetsEstimated;

    /// <summary>
    /// Indicates if this row represents financial expense for the period.
    /// </summary>
    public bool IsFinancialExpense => RowCode == FinancingDetailRowCode.FinancialExpenseCurrent || RowCode == FinancingDetailRowCode.FinancialExpenseEstimated;

    /// <summary>
    /// Gets the bank facilities row from a collection.
    /// </summary>
    /// <param name="items">Collection of financing items.</param>
    /// <returns>Bank facilities row if exists, null otherwise.</returns>
    public static FinancingDetailItem? GetBankFacilities(IEnumerable<FinancingDetailItem> items)
    {
        return items.FirstOrDefault(x => x.IsBankFacilities);
    }

    /// <summary>
    /// Gets the settled facilities row from a collection.
    /// </summary>
    /// <param name="items">Collection of financing items.</param>
    /// <returns>Settled facilities row if exists, null otherwise.</returns>
    public static FinancingDetailItem? GetSettledFacilities(IEnumerable<FinancingDetailItem> items)
    {
        return items.FirstOrDefault(x => x.IsSettledFacilities);
    }

    /// <summary>
    /// Gets the total sum row from a collection.
    /// </summary>
    /// <param name="items">Collection of financing items.</param>
    /// <returns>Total row if exists, null otherwise.</returns>
    public static FinancingDetailItem? GetTotal(IEnumerable<FinancingDetailItem> items)
    {
        return items.FirstOrDefault(x => x.IsTotal);
    }

    /// <summary>
    /// Gets the transfer to assets row from a collection.
    /// </summary>
    /// <param name="items">Collection of financing items.</param>
    /// <returns>Transfer to assets row if exists, null otherwise.</returns>
    public static FinancingDetailItem? GetTransferToAssets(IEnumerable<FinancingDetailItem> items)
    {
        return items.FirstOrDefault(x => x.IsTransferToAssets);
    }

    /// <summary>
    /// Gets the financial expense row from a collection.
    /// </summary>
    /// <param name="items">Collection of financing items.</param>
    /// <returns>Financial expense row if exists, null otherwise.</returns>
    public static FinancingDetailItem? GetFinancialExpense(IEnumerable<FinancingDetailItem> items)
    {
        return items.FirstOrDefault(x => x.IsFinancialExpense);
    }
}
