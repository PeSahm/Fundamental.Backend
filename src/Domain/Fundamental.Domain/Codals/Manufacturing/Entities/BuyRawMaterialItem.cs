namespace Fundamental.Domain.Codals.Manufacturing.Entities;

using Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Represents a raw material purchase item.
/// Based on V5 buyRawMaterial section with columnIds starting with 346xx.
/// </summary>
public class BuyRawMaterialItem
{
    /// <summary>
    /// Description/name of the raw material (e.g., "مارل", "سنگ آهک").
    /// شرح
    /// Maps to V5 value_34641.
    /// </summary>
    public string MaterialName { get; set; }

    /// <summary>
    /// Unit of measurement (e.g., "تن", "کیلوگرم").
    /// واحد
    /// Maps to V5 value_34642.
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Year-to-date quantity purchased (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مقدار
    /// Maps to V5 value_34643.
    /// </summary>
    public decimal? YearToDateQuantity { get; set; }

    /// <summary>
    /// Year-to-date purchase rate in Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - نرخ (ریال)
    /// Maps to V5 value_34644.
    /// </summary>
    public decimal? YearToDateRate { get; set; }

    /// <summary>
    /// Year-to-date purchase amount in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مبلغ (میلیون ریال)
    /// Maps to V5 value_34645.
    /// </summary>
    public decimal? YearToDateAmount { get; set; }

    /// <summary>
    /// Quantity corrections.
    /// اصلاحات - مقدار
    /// Maps to V5 value_34646.
    /// </summary>
    public decimal? CorrectionQuantity { get; set; }

    /// <summary>
    /// Rate corrections in Rials.
    /// اصلاحات - نرخ (ریال)
    /// Maps to V5 value_34647.
    /// </summary>
    public decimal? CorrectionRate { get; set; }

    /// <summary>
    /// Amount corrections in million Rials.
    /// اصلاحات - مبلغ (میلیون ریال)
    /// Maps to V5 value_34648.
    /// </summary>
    public decimal? CorrectionAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date quantity (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مقدار
    /// Maps to V5 value_34649.
    /// </summary>
    public decimal? CorrectedYearToDateQuantity { get; set; }

    /// <summary>
    /// Corrected year-to-date rate in Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - نرخ (ریال)
    /// Maps to V5 value_346410.
    /// </summary>
    public decimal? CorrectedYearToDateRate { get; set; }

    /// <summary>
    /// Corrected year-to-date amount in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مبلغ (میلیون ریال)
    /// Maps to V5 value_346411.
    /// </summary>
    public decimal? CorrectedYearToDateAmount { get; set; }

    /// <summary>
    /// Monthly purchase quantity (one month period ending in current period).
    /// خرید دوره یک ماهه منتهی به دوره جاری - مقدار
    /// Maps to V5 value_346412.
    /// </summary>
    public decimal? MonthlyPurchaseQuantity { get; set; }

    /// <summary>
    /// Monthly purchase rate in Rials (one month period ending in current period).
    /// خرید دوره یک ماهه منتهی به دوره جاری - نرخ (ریال)
    /// Maps to V5 value_346413.
    /// </summary>
    public decimal? MonthlyPurchaseRate { get; set; }

    /// <summary>
    /// Monthly purchase amount in million Rials (one month period ending in current period).
    /// خرید دوره یک ماهه منتهی به دوره جاری - مبلغ (میلیون ریال)
    /// Maps to V5 value_346414.
    /// </summary>
    public decimal? MonthlyPurchaseAmount { get; set; }

    /// <summary>
    /// Cumulative quantity purchased from beginning to end of current period.
    /// مواد اولیه خریداری شده از ابتدای دوره تا پایان دوره منتهی به دوره جاری - مقدار
    /// Maps to V5 value_346415.
    /// </summary>
    public decimal? CumulativeToPeriodQuantity { get; set; }

    /// <summary>
    /// Cumulative purchase rate in Rials from beginning to end of current period.
    /// مواد اولیه خریداری شده از ابتدای دوره تا پایان دوره منتهی به دوره جاری - نرخ (ریال)
    /// Maps to V5 value_346416.
    /// </summary>
    public decimal? CumulativeToPeriodRate { get; set; }

    /// <summary>
    /// Cumulative purchase amount in million Rials from beginning to end of current period.
    /// مواد اولیه خریداری شده از ابتدای دوره تا پایان دوره منتهی به دوره جاری - مبلغ (میلیون ریال)
    /// Maps to V5 value_346417.
    /// </summary>
    public decimal? CumulativeToPeriodAmount { get; set; }

    /// <summary>
    /// Previous year quantity (same period in previous fiscal year).
    /// از ابتدای سال مالی تا تاریخ مشابه سال قبل - مقدار
    /// Maps to V5 value_346418.
    /// </summary>
    public decimal? PreviousYearQuantity { get; set; }

    /// <summary>
    /// Previous year purchase rate in Rials (same period in previous fiscal year).
    /// از ابتدای سال مالی تا تاریخ مشابه سال قبل - نرخ (ریال)
    /// Maps to V5 value_346419.
    /// </summary>
    public decimal? PreviousYearRate { get; set; }

    /// <summary>
    /// Previous year purchase amount in million Rials (same period in previous fiscal year).
    /// از ابتدای سال مالی تا تاریخ مشابه سال قبل - مبلغ (میلیون ریال)
    /// Maps to V5 value_346420.
    /// </summary>
    public decimal? PreviousYearAmount { get; set; }

    /// <summary>
    /// Row code for classification.
    /// -1 = Data row (actual material), 21 = Domestic sum, 24 = Imported sum, 25 = Total sum.
    /// Maps to V5 rowCode field.
    /// </summary>
    public BuyRawMaterialRowCode RowCode { get; set; } = BuyRawMaterialRowCode.Data;

    /// <summary>
    /// Category indicating source of raw material (domestic, imported, or total).
    /// 1 = Domestic (داخلی), 2 = Imported (وارداتی), 0 = Total (جمع).
    /// Maps to V5 category field.
    /// </summary>
    public BuyRawMaterialCategory Category { get; set; } = BuyRawMaterialCategory.Domestic;

    /// <summary>
    /// Gets a value indicating whether this is a data row (actual material entry).
    /// Data rows have RowCode = -1 and contain actual material purchase details.
    /// </summary>
    public bool IsDataRow => RowCode == BuyRawMaterialRowCode.Data;

    /// <summary>
    /// Gets a value indicating whether this is a summary row (sum/total).
    /// Summary rows have RowCode >= 21 and contain aggregated data.
    /// </summary>
    public bool IsSummaryRow => RowCode != BuyRawMaterialRowCode.Data;
}