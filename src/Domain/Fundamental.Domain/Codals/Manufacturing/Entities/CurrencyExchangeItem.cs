namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents currency exchange information for export sales.
/// Based on V5 sourceUsesCurrency section.
/// </summary>
public class CurrencyExchangeItem
{
    /// <summary>
    /// Description of the currency exchange item (e.g., "فروش صادراتی").
    /// Based on V5 value_36401.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Currency type (e.g., "دلار").
    /// Based on V5 value_36402.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Year-to-date foreign currency amount.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 - مبلغ ارزی".
    /// </summary>
    public decimal? YearToDateForeignAmount { get; set; }

    /// <summary>
    /// Year-to-date exchange rate (ریال).
    /// Based on V5 caption "نرخ ارز(ریال)".
    /// </summary>
    public decimal? YearToDateExchangeRate { get; set; }

    /// <summary>
    /// Year-to-date Rial amount (میلیون ریال).
    /// Based on V5 caption "مبلغ ریالی(میلیون ریال)".
    /// </summary>
    public decimal? YearToDateRialAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date foreign currency amount.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 (اصلاح شده) - مبلغ ارزی".
    /// </summary>
    public decimal? CorrectedYearToDateForeignAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date exchange rate (ریال).
    /// Based on V5 caption "نرخ ارز(ریال)".
    /// </summary>
    public decimal? CorrectedYearToDateExchangeRate { get; set; }

    /// <summary>
    /// Corrected year-to-date Rial amount (میلیون ریال).
    /// Based on V5 caption "مبلغ ریالی(میلیون ریال)".
    /// </summary>
    public decimal? CorrectedYearToDateRialAmount { get; set; }

    /// <summary>
    /// Monthly foreign currency amount.
    /// Based on V5 caption "دوره یکماهه 1404/07/30 - مبلغ ارزی".
    /// </summary>
    public decimal? MonthlyForeignAmount { get; set; }

    /// <summary>
    /// Monthly exchange rate (ریال).
    /// Based on V5 caption "نرخ ارز(ریال)".
    /// </summary>
    public decimal? MonthlyExchangeRate { get; set; }

    /// <summary>
    /// Monthly Rial amount (میلیون ریال).
    /// Based on V5 caption "مبلغ ریالی(میلیون ریال)".
    /// </summary>
    public decimal? MonthlyRialAmount { get; set; }

    /// <summary>
    /// Cumulative foreign currency amount to period end.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/07/30 - مبلغ ارزی".
    /// </summary>
    public decimal? CumulativeToPeriodForeignAmount { get; set; }

    /// <summary>
    /// Cumulative exchange rate to period end (ریال).
    /// Based on V5 caption "نرخ ارز(ریال)".
    /// </summary>
    public decimal? CumulativeToPeriodExchangeRate { get; set; }

    /// <summary>
    /// Cumulative Rial amount to period end (میلیون ریال).
    /// Based on V5 caption "مبلغ ریالی(میلیون ریال)".
    /// </summary>
    public decimal? CumulativeToPeriodRialAmount { get; set; }

    /// <summary>
    /// Previous year foreign currency amount.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1403/07/30 - مبلغ ارزی".
    /// </summary>
    public decimal? PreviousYearForeignAmount { get; set; }

    /// <summary>
    /// Previous year exchange rate (ریال).
    /// Based on V5 caption "نرخ ارز(ریال)".
    /// </summary>
    public decimal? PreviousYearExchangeRate { get; set; }

    /// <summary>
    /// Previous year Rial amount (میلیون ریال).
    /// Based on V5 caption "مبلغ ریالی(میلیون ریال)".
    /// </summary>
    public decimal? PreviousYearRialAmount { get; set; }

    /// <summary>
    /// Forecast remaining foreign currency amount.
    /// Based on V5 caption "برآورد مانده تا پایان سال مالی 1404/12/29 - مبلغ ارزی".
    /// </summary>
    public decimal? ForecastRemainingForeignAmount { get; set; }

    /// <summary>
    /// Forecast remaining exchange rate (ریال).
    /// Based on V5 caption "نرخ ارز(ریال)".
    /// </summary>
    public decimal? ForecastRemainingExchangeRate { get; set; }

    /// <summary>
    /// Forecast remaining Rial amount (میلیون ریال).
    /// Based on V5 caption "مبلغ ریالی(میلیون ریال)".
    /// </summary>
    public decimal? ForecastRemainingRialAmount { get; set; }

    /// <summary>
    /// Category (1 for sources, 2 for uses, 0 for totals).
    /// </summary>
    public int? Category { get; set; }
}