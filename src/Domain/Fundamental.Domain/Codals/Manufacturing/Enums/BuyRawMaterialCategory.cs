namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Categories for raw material purchase items in Monthly Activity reports.
/// Represents the source of raw materials (domestic vs. imported).
/// </summary>
public enum BuyRawMaterialCategory
{
    /// <summary>
    /// Total/sum of all raw material purchases.
    /// جمع کل.
    /// </summary>
    Total = 0,

    /// <summary>
    /// Domestic raw materials purchased within Iran.
    /// مواد اولیه داخلی.
    /// </summary>
    Domestic = 1,

    /// <summary>
    /// Imported raw materials purchased from abroad.
    /// مواد اولیه وارداتی (خارجی).
    /// </summary>
    Imported = 2
}
