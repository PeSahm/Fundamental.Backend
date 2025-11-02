using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents a production and sales item.
/// Based on V5 captions like "از ابتدای سال مالی تا تاریخ 1404/06/31 - تعداد تولید".
/// Can represent either data rows (rowCode = -1) or summary rows (rowCode > 0).
/// </summary>
public class ProductionAndSalesItem
{
    /// <summary>
    /// Name of the product (e.g., "کلینکر").
    /// Based on V5 value_11971.
    /// </summary>
    public string ProductName { get; set; }

    /// <summary>
    /// Unit of measurement (e.g., "تن").
    /// Based on V5 value_11972.
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Year-to-date production quantity (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - تعداد تولید
    /// Maps to V5 value_11973.
    /// </summary>
    public decimal? YearToDateProductionQuantity { get; set; }

    /// <summary>
    /// Year-to-date sales quantity (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - تعداد فروش
    /// Maps to V5 value_11974.
    /// </summary>
    public decimal? YearToDateSalesQuantity { get; set; }

    /// <summary>
    /// Year-to-date sales rate in Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - نرخ فروش (ریال)
    /// Maps to V5 value_11975.
    /// </summary>
    public decimal? YearToDateSalesRate { get; set; }

    /// <summary>
    /// Year-to-date sales amount in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مبلغ فروش (میلیون ریال)
    /// Maps to V5 value_11976.
    /// </summary>
    public decimal? YearToDateSalesAmount { get; set; }

    /// <summary>
    /// Production quantity corrections.
    /// اصلاحات - تعداد تولید
    /// Maps to V5 value_11977.
    /// </summary>
    public decimal? CorrectionProductionQuantity { get; set; }

    /// <summary>
    /// Sales quantity corrections.
    /// اصلاحات - تعداد فروش
    /// Maps to V5 value_11978.
    /// </summary>
    public decimal? CorrectionSalesQuantity { get; set; }

    /// <summary>
    /// Sales amount corrections in million Rials.
    /// اصلاحات - مبلغ فروش (میلیون ریال)
    /// Maps to V5 value_11979.
    /// </summary>
    public decimal? CorrectionSalesAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date production quantity (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - تعداد تولید
    /// Maps to V5 value_119710.
    /// </summary>
    public decimal? CorrectedYearToDateProductionQuantity { get; set; }

    /// <summary>
    /// Corrected year-to-date sales quantity (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - تعداد فروش
    /// Maps to V5 value_119711.
    /// </summary>
    public decimal? CorrectedYearToDateSalesQuantity { get; set; }

    /// <summary>
    /// Corrected year-to-date sales rate in Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - نرخ فروش (ریال)
    /// Maps to V5 value_119712.
    /// </summary>
    public decimal? CorrectedYearToDateSalesRate { get; set; }

    /// <summary>
    /// Corrected year-to-date sales amount in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مبلغ فروش (میلیون ریال)
    /// Maps to V5 value_119713.
    /// </summary>
    public decimal? CorrectedYearToDateSalesAmount { get; set; }

    /// <summary>
    /// Monthly production quantity (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - تعداد تولید
    /// Maps to V5 value_119714.
    /// </summary>
    public decimal? MonthlyProductionQuantity { get; set; }

    /// <summary>
    /// Monthly sales quantity (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - تعداد فروش
    /// Maps to V5 value_119715.
    /// </summary>
    public decimal? MonthlySalesQuantity { get; set; }

    /// <summary>
    /// Monthly sales rate in Rials (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - نرخ فروش (ریال)
    /// Maps to V5 value_119716.
    /// </summary>
    public decimal? MonthlySalesRate { get; set; }

    /// <summary>
    /// Monthly sales amount in million Rials (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - مبلغ فروش (میلیون ریال)
    /// Maps to V5 value_119717.
    /// </summary>
    public decimal? MonthlySalesAmount { get; set; }

    /// <summary>
    /// Cumulative production quantity from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - تعداد تولید
    /// Maps to V5 value_119718.
    /// </summary>
    public decimal? CumulativeToPeriodProductionQuantity { get; set; }

    /// <summary>
    /// Cumulative sales quantity from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - تعداد فروش
    /// Maps to V5 value_119719.
    /// </summary>
    public decimal? CumulativeToPeriodSalesQuantity { get; set; }

    /// <summary>
    /// Cumulative sales rate in Rials from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - نرخ فروش (ریال)
    /// Maps to V5 value_119720.
    /// </summary>
    public decimal? CumulativeToPeriodSalesRate { get; set; }

    /// <summary>
    /// Cumulative sales amount in million Rials from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - مبلغ فروش (میلیون ریال)
    /// Maps to V5 value_119721.
    /// </summary>
    public decimal? CumulativeToPeriodSalesAmount { get; set; }

    /// <summary>
    /// Previous year production quantity (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - تعداد تولید
    /// Maps to V5 value_119722.
    /// </summary>
    public decimal? PreviousYearProductionQuantity { get; set; }

    /// <summary>
    /// Previous year sales quantity (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - تعداد فروش
    /// Maps to V5 value_119723.
    /// </summary>
    public decimal? PreviousYearSalesQuantity { get; set; }

    /// <summary>
    /// Previous year sales rate in Rials (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - نرخ فروش (ریال)
    /// Maps to V5 value_119724.
    /// </summary>
    public decimal? PreviousYearSalesRate { get; set; }

    /// <summary>
    /// Previous year sales amount in million Rials (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - مبلغ فروش (میلیون ریال)
    /// Maps to V5 value_119725.
    /// </summary>
    public decimal? PreviousYearSalesAmount { get; set; }

    /// <summary>
    /// Product status or type (e.g., "تولید" for production).
    /// وضعیت محصول
    /// Maps to V5 value_119726.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Row code indicating if this is a data row (-1) or summary row (5, 8, 11, 14, 15, 16).
    /// Used to identify internal sale sum, export sale sum, total sum, etc.
    /// Maps to V5 rowCode field.
    /// </summary>
    public ProductionSalesRowCode RowCode { get; set; } = ProductionSalesRowCode.Data;

    /// <summary>
    /// Category indicating the type of sale or summary.
    /// 0 = Sum, 1 = Internal, 2 = Export, 3 = Service, 4 = Return, 5 = Discount.
    /// Maps to V5 category field.
    /// </summary>
    public ProductionSalesCategory Category { get; set; } = ProductionSalesCategory.Internal;

    /// <summary>
    /// Indicates if this is a data row (true) or a summary row (false).
    /// Data rows have RowCode = -1, summary rows have RowCode >= 0.
    /// </summary>
    public bool IsDataRow => RowCode == ProductionSalesRowCode.Data;

    /// <summary>
    /// Indicates if this is a summary row (true) or a data row (false).
    /// Summary rows have RowCode >= 0, data rows have RowCode = -1.
    /// </summary>
    public bool IsSummaryRow => !IsDataRow;
}