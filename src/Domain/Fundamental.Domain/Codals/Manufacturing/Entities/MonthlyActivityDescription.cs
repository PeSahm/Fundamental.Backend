namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents descriptive/explanatory text for monthly activity report.
/// Based on V5 productMonthlyActivityDesc1 section.
/// </summary>
public class MonthlyActivityDescription
{
    /// <summary>
    /// Row code identifier for classification.
    /// Maps to V5 rowCode field.
    /// </summary>
    public int? RowCode { get; set; }

    /// <summary>
    /// Description text content with explanatory information.
    /// توضیحات
    /// Maps to V5 value_11991.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Category/type of description for grouping.
    /// Maps to V5 category field.
    /// </summary>
    public int? Category { get; set; }

    /// <summary>
    /// Type of row (e.g., "FixedRow", "CustomRow") indicating if it's a predefined or custom description.
    /// Maps to V5 rowType field.
    /// </summary>
    public string RowType { get; set; }
}