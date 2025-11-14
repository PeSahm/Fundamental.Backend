namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Represents the category of production and sales data.
/// </summary>
public enum ProductionSalesCategory
{
    /// <summary>
    /// Summary/Total row.
    /// </summary>
    Sum = 0,

    /// <summary>
    /// Internal (domestic) sale.
    /// </summary>
    Internal = 1,

    /// <summary>
    /// Export sale.
    /// </summary>
    Export = 2,

    /// <summary>
    /// Service income.
    /// </summary>
    Service = 3,

    /// <summary>
    /// Return sale.
    /// </summary>
    Return = 4,

    /// <summary>
    /// Discount.
    /// </summary>
    Discount = 5
}