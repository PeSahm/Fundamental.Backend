using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents currency exchange information for foreign currency sources and uses.
/// اطلاعات منابع و مصارف ارزی شرکت
/// Based on V5 sourceUsesCurrency section with columnIds starting with 364xx.
/// </summary>
public class CurrencyExchangeItem
{
    /// <summary>
    /// Description of foreign currency sources (e.g., "فروش صادراتی", "وام خارجی").
    /// منابع ارزی
    /// Maps to V5 value_36401.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Currency type (e.g., "دلار", "یورو").
    /// نوع ارز
    /// Maps to V5 value_36402.
    /// </summary>
    public string Currency { get; set; }

    /// <summary>
    /// Year-to-date foreign currency amount (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مبلغ ارزی
    /// Maps to V5 value_36403.
    /// </summary>
    public decimal? YearToDateForeignAmount { get; set; }

    /// <summary>
    /// Year-to-date exchange rate (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - نرخ ارز
    /// Maps to V5 value_36404.
    /// </summary>
    public decimal? YearToDateExchangeRate { get; set; }

    /// <summary>
    /// Year-to-date Rial amount in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_36405.
    /// </summary>
    public decimal? YearToDateRialAmount { get; set; }

    /// <summary>
    /// Foreign currency amount corrections.
    /// اصلاحات - مبلغ ارزی
    /// Maps to V5 value_36406.
    /// </summary>
    public decimal? CorrectionForeignAmount { get; set; }

    /// <summary>
    /// Exchange rate corrections.
    /// اصلاحات - نرخ ارز
    /// Maps to V5 value_36407.
    /// </summary>
    public decimal? CorrectionExchangeRate { get; set; }

    /// <summary>
    /// Rial amount corrections in million Rials.
    /// اصلاحات - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_36408.
    /// </summary>
    public decimal? CorrectionRialAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date foreign currency amount (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مبلغ ارزی
    /// Maps to V5 value_36409.
    /// </summary>
    public decimal? CorrectedYearToDateForeignAmount { get; set; }

    /// <summary>
    /// Corrected year-to-date exchange rate (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - نرخ ارز
    /// Maps to V5 value_364010.
    /// </summary>
    public decimal? CorrectedYearToDateExchangeRate { get; set; }

    /// <summary>
    /// Corrected year-to-date Rial amount in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_364011.
    /// </summary>
    public decimal? CorrectedYearToDateRialAmount { get; set; }

    /// <summary>
    /// Monthly foreign currency amount (one month period).
    /// دوره یکماهه - دوره جاری - مبلغ ارزی
    /// Maps to V5 value_364012.
    /// </summary>
    public decimal? MonthlyForeignAmount { get; set; }

    /// <summary>
    /// Monthly exchange rate (one month period).
    /// دوره یکماهه - دوره جاری - نرخ ارز
    /// Maps to V5 value_364013.
    /// </summary>
    public decimal? MonthlyExchangeRate { get; set; }

    /// <summary>
    /// Monthly Rial amount in million Rials (one month period).
    /// دوره یکماهه - دوره جاری - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_364014.
    /// </summary>
    public decimal? MonthlyRialAmount { get; set; }

    /// <summary>
    /// Cumulative foreign currency amount from beginning to end of current period.
    /// از ابتدای سال مالی تا تاریخ منتهی به دوره جاری - مبلغ ارزی
    /// Maps to V5 value_364015.
    /// </summary>
    public decimal? CumulativeToPeriodForeignAmount { get; set; }

    /// <summary>
    /// Cumulative exchange rate from beginning to end of current period.
    /// از ابتدای سال مالی تا تاریخ منتهی به دوره جاری - نرخ ارز
    /// Maps to V5 value_364016.
    /// </summary>
    public decimal? CumulativeToPeriodExchangeRate { get; set; }

    /// <summary>
    /// Cumulative Rial amount in million Rials from beginning to end of current period.
    /// از ابتدای سال مالی تا تاریخ منتهی به دوره جاری - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_364017.
    /// </summary>
    public decimal? CumulativeToPeriodRialAmount { get; set; }

    /// <summary>
    /// Previous year foreign currency amount (same period in previous fiscal year).
    /// از ابتدای سال مالی تا تاریخ منتهی به دوره مشابه سال قبل - مبلغ ارزی
    /// Maps to V5 value_364018.
    /// </summary>
    public decimal? PreviousYearForeignAmount { get; set; }

    /// <summary>
    /// Previous year exchange rate (same period in previous fiscal year).
    /// از ابتدای سال مالی تا تاریخ منتهی به دوره مشابه سال قبل - نرخ ارز
    /// Maps to V5 value_364019.
    /// </summary>
    public decimal? PreviousYearExchangeRate { get; set; }

    /// <summary>
    /// Previous year Rial amount in million Rials (same period in previous fiscal year).
    /// از ابتدای سال مالی تا تاریخ منتهی به دوره مشابه سال قبل - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_364020.
    /// </summary>
    public decimal? PreviousYearRialAmount { get; set; }

    /// <summary>
    /// Forecast remaining foreign currency amount until end of fiscal year.
    /// برآورد مانده تا پایان سال مالی - مبلغ ارزی
    /// Maps to V5 value_364021.
    /// </summary>
    public decimal? ForecastRemainingForeignAmount { get; set; }

    /// <summary>
    /// Forecast remaining exchange rate until end of fiscal year.
    /// برآورد مانده تا پایان سال مالی - نرخ ارز
    /// Maps to V5 value_364022.
    /// </summary>
    public decimal? ForecastRemainingExchangeRate { get; set; }

    /// <summary>
    /// Forecast remaining Rial amount in million Rials until end of fiscal year.
    /// برآورد مانده تا پایان سال مالی - مبلغ ریالی(میلیون ریال)
    /// Maps to V5 value_364023.
    /// </summary>
    public decimal? ForecastRemainingRialAmount { get; set; }

    /// <summary>
    /// Row code for classification (Data=-1, OtherSources=32, SourcesSum=33, OtherUses=36, UsesSum=37).
    /// کد ردیف برای تمایز بین داده‌ها و جمع‌بندی‌ها
    /// Maps to V5 rowCode field.
    /// </summary>
    public CurrencyExchangeRowCode RowCode { get; set; } = CurrencyExchangeRowCode.Data;

    /// <summary>
    /// Category (Total=0, Sources=1, Uses=2).
    /// دسته‌بندی (منابع، مصارف، یا جمع کل)
    /// Maps to V5 category field.
    /// </summary>
    public CurrencyExchangeCategory Category { get; set; } = CurrencyExchangeCategory.Sources;

    /// <summary>
    /// Indicates if this is a data row (not a summary/total row).
    /// آیا این ردیف داده است (نه جمع‌بندی).
    /// </summary>
    public bool IsDataRow => RowCode == CurrencyExchangeRowCode.Data;

    /// <summary>
    /// Indicates if this is a summary row (total/sum row).
    /// آیا این ردیف جمع‌بندی است.
    /// </summary>
    public bool IsSummaryRow => RowCode != CurrencyExchangeRowCode.Data;

    /// <summary>
    /// Indicates if this is a sources row (Category=Sources).
    /// آیا این ردیف مربوط به منابع ارزی است.
    /// </summary>
    public bool IsSourcesRow => Category == CurrencyExchangeCategory.Sources;

    /// <summary>
    /// Indicates if this is a uses row (Category=Uses).
    /// آیا این ردیف مربوط به مصارف ارزی است.
    /// </summary>
    public bool IsUsesRow => Category == CurrencyExchangeCategory.Uses;
}