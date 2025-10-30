namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents descriptive/explanatory text for monthly activity.
/// Based on V5 productMonthlyActivityDesc1 section.
/// </summary>
public class MonthlyActivityDescription
{
    /// <summary>
    /// Row code identifier.
    /// </summary>
    public int? RowCode { get; set; }

    /// <summary>
    /// Description text content.
    /// Based on V5 value_11991.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Category/type of description.
    /// </summary>
    public int? Category { get; set; }

    /// <summary>
    /// Type of row (FixedRow, CustomRow, etc.).
    /// </summary>
    public string RowType { get; set; }
}