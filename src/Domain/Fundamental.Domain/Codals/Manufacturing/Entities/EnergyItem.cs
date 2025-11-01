namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents energy consumption and cost information.
/// Based on V5 energy section with columnIds starting with 319xx.
/// </summary>
public class EnergyItem
{
    /// <summary>
    /// Industry classification (e.g., "صنعت سیمان").
    /// صنعت
    /// Maps to V5 value_31951.
    /// </summary>
    public string Industry { get; set; }

    /// <summary>
    /// Energy classification category.
    /// طبقه بندی
    /// Maps to V5 value_31952.
    /// </summary>
    public string Classification { get; set; }

    /// <summary>
    /// Type of energy (e.g., "برق", "گاز").
    /// نوع انرژی
    /// Maps to V5 value_31953.
    /// </summary>
    public string EnergyType { get; set; }

    /// <summary>
    /// Unit of measurement (e.g., "کیلووات ساعت", "مترمکعب").
    /// واحد اندازه گیری
    /// Maps to V5 value_31954.
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Year-to-date consumption amount (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مقدار مصرف
    /// Maps to V5 value_31955.
    /// </summary>
    public decimal? YearToDateConsumption { get; set; }

    /// <summary>
    /// Year-to-date energy rate in Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - نرخ - ریال
    /// Maps to V5 value_31956.
    /// </summary>
    public decimal? YearToDateRate { get; set; }

    /// <summary>
    /// Year-to-date energy cost in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - مبلغ - میلیون ریال
    /// Maps to V5 value_31957.
    /// </summary>
    public decimal? YearToDateCost { get; set; }

    /// <summary>
    /// Consumption amount corrections.
    /// اصلاحات - مقدار مصرف
    /// Maps to V5 value_31958.
    /// </summary>
    public decimal? CorrectionConsumption { get; set; }

    /// <summary>
    /// Rate corrections in Rials.
    /// اصلاحات - نرخ - ریال
    /// Maps to V5 value_31959.
    /// </summary>
    public decimal? CorrectionRate { get; set; }

    /// <summary>
    /// Cost corrections in million Rials.
    /// اصلاحات - مبلغ - میلیون ریال
    /// Maps to V5 value_319510.
    /// </summary>
    public decimal? CorrectionCost { get; set; }

    /// <summary>
    /// Corrected year-to-date consumption amount (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مقدار مصرف
    /// Maps to V5 value_319511.
    /// </summary>
    public decimal? CorrectedYearToDateConsumption { get; set; }

    /// <summary>
    /// Corrected year-to-date energy rate in Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - نرخ - ریال
    /// Maps to V5 value_319512.
    /// </summary>
    public decimal? CorrectedYearToDateRate { get; set; }

    /// <summary>
    /// Corrected year-to-date energy cost in million Rials (up to end of previous period).
    /// از ابتدای سال مالی تا پایان دوره قبل - اصلاح شده - مبلغ - میلیون ریال
    /// Maps to V5 value_319513.
    /// </summary>
    public decimal? CorrectedYearToDateCost { get; set; }

    /// <summary>
    /// Monthly consumption amount (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - مقدار مصرف
    /// Maps to V5 value_319514.
    /// </summary>
    public decimal? MonthlyConsumption { get; set; }

    /// <summary>
    /// Monthly energy rate in Rials (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - نرخ - ریال
    /// Maps to V5 value_319515.
    /// </summary>
    public decimal? MonthlyRate { get; set; }

    /// <summary>
    /// Monthly energy cost in million Rials (one month period for current month).
    /// دوره یک ماهه مربوط به ماه جاری - مبلغ - میلیون ریال
    /// Maps to V5 value_319516.
    /// </summary>
    public decimal? MonthlyCost { get; set; }

    /// <summary>
    /// Cumulative consumption amount from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - مقدار مصرف
    /// Maps to V5 value_319517.
    /// </summary>
    public decimal? CumulativeToPeriodConsumption { get; set; }

    /// <summary>
    /// Cumulative energy rate in Rials from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - نرخ - ریال
    /// Maps to V5 value_319518.
    /// </summary>
    public decimal? CumulativeToPeriodRate { get; set; }

    /// <summary>
    /// Cumulative energy cost in million Rials from beginning of year to end of current period.
    /// از ابتدای سال تا پایان دوره جاری - مبلغ - میلیون ریال
    /// Maps to V5 value_319519.
    /// </summary>
    public decimal? CumulativeToPeriodCost { get; set; }

    /// <summary>
    /// Previous year consumption amount (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - مقدار
    /// Maps to V5 value_319520.
    /// </summary>
    public decimal? PreviousYearConsumption { get; set; }

    /// <summary>
    /// Previous year energy rate in Rials (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - نرخ - ریال
    /// Maps to V5 value_319521.
    /// </summary>
    public decimal? PreviousYearRate { get; set; }

    /// <summary>
    /// Previous year energy cost in million Rials (same period in previous fiscal year).
    /// از ابتدای سال تا پایان دوره جاری - سال مالی قبل - مبلغ
    /// Maps to V5 value_319522.
    /// </summary>
    public decimal? PreviousYearCost { get; set; }

    /// <summary>
    /// Forecast consumption for current fiscal year.
    /// پیش‌بینی سال مالی جاری - مقدار مصرف
    /// Maps to V5 value_319523.
    /// </summary>
    public decimal? ForecastYearConsumption { get; set; }

    /// <summary>
    /// Explanation for changes in consumption amount.
    /// توضیحات در خصوص علت تغییر میزان مصرف
    /// Maps to V5 value_319524.
    /// </summary>
    public string ConsumptionChangeExplanation { get; set; }

    /// <summary>
    /// Row code for classification.
    /// Maps to V5 rowCode field.
    /// </summary>
    public int? RowCode { get; set; }

    /// <summary>
    /// Category for classification.
    /// Maps to V5 category field.
    /// </summary>
    public int? Category { get; set; }
}