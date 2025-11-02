namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Row codes for raw material purchase items in Monthly Activity reports.
/// Distinguishes between data rows and various summary rows.
/// </summary>
public enum BuyRawMaterialRowCode
{
    /// <summary>
    /// Data row containing actual raw material purchase details.
    /// ردیف داده - اطلاعات خرید مواد اولیه.
    /// </summary>
    Data = -1,

    /// <summary>
    /// Sum of domestic (internal) raw material purchases.
    /// جمع مواد اولیه داخلی.
    /// </summary>
    DomesticSum = 21,

    /// <summary>
    /// Sum of imported raw material purchases.
    /// جمع مواد اولیه وارداتی.
    /// </summary>
    ImportedSum = 24,

    /// <summary>
    /// Total sum of all raw material purchases (domestic + imported).
    /// جمع کل.
    /// </summary>
    TotalSum = 25
}
