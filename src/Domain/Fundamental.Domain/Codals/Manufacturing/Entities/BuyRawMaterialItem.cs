namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents a buy raw material item.
/// Based on V5 captions like "از ابتدای سال مالی تا تاریخ 1404/06/31 - مقدار" (ignoring date).
/// </summary>
public class BuyRawMaterialItem
{
    /// <summary>
    /// Name of the material (e.g., "مارل").
    /// Based on V5 value_34641.
    /// </summary>
    public string MaterialName { get; set; }

    /// <summary>
    /// Unit of measurement (e.g., "تن").
    /// Based on V5 value_34642.
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Year-to-date quantity.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 - مقدار".
    /// </summary>
    public decimal? YearToDateQuantity { get; set; }

    /// <summary>
    /// Year-to-date rate (ریال).
    /// Based on V5 caption "نرخ (ریال)".
    /// </summary>
    public decimal? YearToDateRate { get; set; }

    /// <summary>
    /// Year-to-date amount (میلیون ریال).
    /// Based on V5 caption "مبلغ (میلیون ریال)".
    /// </summary>
    public decimal? YearToDateAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date quantity.
    /// Based on V5 caption "ازابتدای سال مالی تا تاریخ 1404/06/31 (اصلاح شده) - مقدار".
    /// </summary>
    public decimal? CorrectedYearToDateQuantity { get; set; }

    /// <summary>
    /// Corrected year-to-date rate (ریال).
    /// Based on V5 caption "نرخ (ريال)".
    /// </summary>
    public decimal? CorrectedYearToDateRate { get; set; }

    /// <summary>
    /// Corrected year-to-date amount (میلیون ریال).
    /// Based on V5 caption "مبلغ (میلیون ریال)".
    /// </summary>
    public decimal? CorrectedYearToDateAmount { get; set; }

    /// <summary>
    /// Monthly purchase quantity.
    /// Based on V5 caption "خرید دوره یک ماهه منتهی به 1404/07/30 - مقدار".
    /// </summary>
    public decimal? MonthlyPurchaseQuantity { get; set; }

    /// <summary>
    /// Monthly purchase rate (ریال).
    /// Based on V5 caption "نرخ (ريال)".
    /// </summary>
    public decimal? MonthlyPurchaseRate { get; set; }

    /// <summary>
    /// Monthly purchase amount (میلیون ریال).
    /// Based on V5 caption "مبلغ (میلیون ریال)".
    /// </summary>
    public decimal? MonthlyPurchaseAmount { get; set; }

    /// <summary>
    /// Cumulative quantity to period end.
    /// Based on V5 caption "مواد اولیه خریداری شده از ابتدای دوره تا پایان دوره منتهی به 1404/07/30 - مقدار".
    /// </summary>
    public decimal? CumulativeToPeriodQuantity { get; set; }

    /// <summary>
    /// Cumulative rate to period end (ریال).
    /// Based on V5 caption "نرخ (ريال)".
    /// </summary>
    public decimal? CumulativeToPeriodRate { get; set; }

    /// <summary>
    /// Cumulative amount to period end (میلیون ریال).
    /// Based on V5 caption "مبلغ (میلیون ریال)".
    /// </summary>
    public decimal? CumulativeToPeriodAmount { get; set; }

    /// <summary>
    /// Previous year quantity.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1403/07/30 - مقدار".
    /// </summary>
    public decimal? PreviousYearQuantity { get; set; }

    /// <summary>
    /// Previous year rate (ریال).
    /// Based on V5 caption "نرخ (ريال)".
    /// </summary>
    public decimal? PreviousYearRate { get; set; }

    /// <summary>
    /// Previous year amount (میلیون ریال).
    /// Based on V5 caption "مبلغ (میلیون ریال)".
    /// </summary>
    public decimal? PreviousYearAmount { get; set; }

    /// <summary>
    /// Category (1 for domestic, 2 for imported, 0 for total).
    /// </summary>
    public int? Category { get; set; }
}