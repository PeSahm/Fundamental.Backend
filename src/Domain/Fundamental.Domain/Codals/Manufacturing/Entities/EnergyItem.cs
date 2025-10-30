namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents an energy item.
/// Based on V5 captions like "از ابتدای سال مالی تا تاریخ 1404/06/31 - مصرف انرژی".
/// </summary>
public class EnergyItem
{
    /// <summary>
    /// Name of the energy type (e.g., "برق").
    /// Based on V5 value_11971.
    /// </summary>
    public string EnergyType { get; set; }

    /// <summary>
    /// Unit of measurement (e.g., "کیلووات ساعت").
    /// Based on V5 value_11972.
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// Year-to-date energy consumption.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 - مصرف انرژی".
    /// </summary>
    public decimal? YearToDateConsumption { get; set; }

    /// <summary>
    /// Year-to-date energy cost (میلیون ریال).
    /// Based on V5 caption "هزینه انرژی (میلیون ریال)".
    /// </summary>
    public decimal? YearToDateCost { get; set; }

    /// <summary>
    /// Corrected year-to-date energy consumption.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/06/31 (اصلاح شده) - مصرف انرژی".
    /// </summary>
    public decimal? CorrectedYearToDateConsumption { get; set; }

    /// <summary>
    /// Corrected year-to-date energy cost (میلیون ریال).
    /// Based on V5 caption "هزینه انرژی (میلیون ریال)".
    /// </summary>
    public decimal? CorrectedYearToDateCost { get; set; }

    /// <summary>
    /// Monthly energy consumption.
    /// Based on V5 caption "دوره یک ماهه منتهی به 1404/07/30 - مصرف انرژی".
    /// </summary>
    public decimal? MonthlyConsumption { get; set; }

    /// <summary>
    /// Monthly energy cost (میلیون ریال).
    /// Based on V5 caption "هزینه انرژی (میلیون ریال)".
    /// </summary>
    public decimal? MonthlyCost { get; set; }

    /// <summary>
    /// Cumulative energy consumption to period end.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1404/07/30 - مصرف انرژی".
    /// </summary>
    public decimal? CumulativeToPeriodConsumption { get; set; }

    /// <summary>
    /// Cumulative energy cost to period end (میلیون ریال).
    /// Based on V5 caption "هزینه انرژی (میلیون ریال)".
    /// </summary>
    public decimal? CumulativeToPeriodCost { get; set; }

    /// <summary>
    /// Previous year energy consumption.
    /// Based on V5 caption "از ابتدای سال مالی تا تاریخ 1403/07/30 - مصرف انرژی".
    /// </summary>
    public decimal? PreviousYearConsumption { get; set; }

    /// <summary>
    /// Previous year energy cost (میلیون ریال).
    /// Based on V5 caption "هزینه انرژی (میلیون ریال)".
    /// </summary>
    public decimal? PreviousYearCost { get; set; }

    /// <summary>
    /// Type (e.g., "انرژی" for energy).
    /// Based on V5 value_119726.
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// Category (1 for domestic, 2 for imported, 0 for total).
    /// </summary>
    public int? Category { get; set; }
}