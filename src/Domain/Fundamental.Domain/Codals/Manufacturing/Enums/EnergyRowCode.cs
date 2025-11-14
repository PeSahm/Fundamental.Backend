namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Row codes for energy consumption items in Monthly Activity reports.
/// Distinguishes between data rows and summary rows.
/// </summary>
public enum EnergyRowCode
{
    /// <summary>
    /// Data row containing actual energy consumption details.
    /// ردیف داده - اطلاعات مصرف انرژی.
    /// </summary>
    Data = -1,

    /// <summary>
    /// Total sum of all energy consumption.
    /// جمع کل مصرف انرژی.
    /// </summary>
    TotalSum = 44
}