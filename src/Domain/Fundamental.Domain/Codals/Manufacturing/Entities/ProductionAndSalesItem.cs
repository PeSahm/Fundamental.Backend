namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents a production and sales item.
/// Based on V5 captions like "از ابتدای سال مالی تا تاریخ 1404/06/31 - تعداد تولید".
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
    /// Year-to-date production quantity.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 - تعداد تولید".
    /// </summary>
    public decimal? YearToDateProductionQuantity { get; set; }

    /// <summary>
    /// Year-to-date sales quantity.
    /// Based on V5 caption "تعداد فروش".
    /// </summary>
    public decimal? YearToDateSalesQuantity { get; set; }

    /// <summary>
    /// Year-to-date sales rate (ریال).
    /// Based on V5 caption "نرخ فروش (ریال)".
    /// </summary>
    public decimal? YearToDateSalesRate { get; set; }

    /// <summary>
    /// Year-to-date sales amount (میلیون ریال).
    /// Based on V5 caption "مبلغ فروش (میلیون ریال)".
    /// </summary>
    public decimal? YearToDateSalesAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date production quantity.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 (اصلاح شده) - تعداد تولید".
    /// </summary>
    public decimal? CorrectedYearToDateProductionQuantity { get; set; }

    /// <summary>
    /// Corrected year-to-date sales quantity.
    /// Based on V5 caption "تعداد فروش".
    /// </summary>
    public decimal? CorrectedYearToDateSalesQuantity { get; set; }

    /// <summary>
    /// Corrected year-to-date sales rate (ریال).
    /// Based on V5 caption "نرخ فروش (ریال)".
    /// </summary>
    public decimal? CorrectedYearToDateSalesRate { get; set; }

    /// <summary>
    /// Corrected year-to-date sales amount (میلیون ریال).
    /// Based on V5 caption "مبلغ فروش (میلیون ریال)".
    /// </summary>
    public decimal? CorrectedYearToDateSalesAmount { get; set; }

    /// <summary>
    /// Monthly production quantity.
    /// Based on V5 caption "دوره یک ماهه منتهی به 1404/07/30 - تعداد تولید".
    /// </summary>
    public decimal? MonthlyProductionQuantity { get; set; }

    /// <summary>
    /// Monthly sales quantity.
    /// Based on V5 caption "تعداد فروش".
    /// </summary>
    public decimal? MonthlySalesQuantity { get; set; }

    /// <summary>
    /// Monthly sales rate (ریال).
    /// Based on V5 caption "نرخ فروش (ریال)".
    /// </summary>
    public decimal? MonthlySalesRate { get; set; }

    /// <summary>
    /// Monthly sales amount (میلیون ریال).
    /// Based on V5 caption "مبلغ فروش (میلیون ریال)".
    /// </summary>
    public decimal? MonthlySalesAmount { get; set; }

    /// <summary>
    /// Cumulative production quantity to period end.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/07/30 - تعداد تولید".
    /// </summary>
    public decimal? CumulativeToPeriodProductionQuantity { get; set; }

    /// <summary>
    /// Cumulative sales quantity to period end.
    /// Based on V5 caption "تعداد فروش".
    /// </summary>
    public decimal? CumulativeToPeriodSalesQuantity { get; set; }

    /// <summary>
    /// Cumulative sales rate to period end (ریال).
    /// Based on V5 caption "نرخ فروش (ریال)".
    /// </summary>
    public decimal? CumulativeToPeriodSalesRate { get; set; }

    /// <summary>
    /// Cumulative sales amount to period end (میلیون ریال).
    /// Based on V5 caption "مبلغ فروش (میلیون ریال)".
    /// </summary>
    public decimal? CumulativeToPeriodSalesAmount { get; set; }

    /// <summary>
    /// Previous year production quantity.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1403/07/30 - تعداد تولید".
    /// </summary>
    public decimal? PreviousYearProductionQuantity { get; set; }

    /// <summary>
    /// Previous year sales quantity.
    /// Based on V5 caption "تعداد فروش".
    /// </summary>
    public decimal? PreviousYearSalesQuantity { get; set; }

    /// <summary>
    /// Previous year sales rate (ریال).
    /// Based on V5 caption "نرخ فروش (ریال)".
    /// </summary>
    public decimal? PreviousYearSalesRate { get; set; }

    /// <summary>
    /// Previous year sales amount (میلیون ریال).
    /// Based on V5 caption "مبلغ فروش (میلیون ریال)".
    /// </summary>
    public decimal? PreviousYearSalesAmount { get; set; }

    /// <summary>
    /// Type (e.g., "تولید" for production).
    /// Based on V5 value_119726.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Category (1 for domestic, 2 for imported, 0 for total).
    /// </summary>
    public int? Category { get; set; }
}