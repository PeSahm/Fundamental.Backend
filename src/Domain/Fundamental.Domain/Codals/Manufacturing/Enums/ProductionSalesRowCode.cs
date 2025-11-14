namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Represents the type of row in production and sales data.
/// Used to identify summary rows vs data rows.
/// </summary>
public enum ProductionSalesRowCode
{
    /// <summary>
    /// Data row (product detail).
    /// </summary>
    Data = -1,

    /// <summary>
    /// Internal sale summary row.
    /// </summary>
    InternalSale = 5,

    /// <summary>
    /// Export sale summary row.
    /// </summary>
    ExportSale = 8,

    /// <summary>
    /// Service income summary row.
    /// </summary>
    ServiceIncome = 11,

    /// <summary>
    /// Return sale summary row.
    /// </summary>
    ReturnSale = 14,

    /// <summary>
    /// Discount summary row.
    /// </summary>
    Discount = 15,

    /// <summary>
    /// Total sum row (grand total).
    /// </summary>
    TotalSum = 16
}