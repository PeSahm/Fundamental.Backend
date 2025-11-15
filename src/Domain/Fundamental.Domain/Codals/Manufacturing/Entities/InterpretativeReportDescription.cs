using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Represents a description or note in the interpretative report summary.
/// Maps to various description sections (p5Desc1, p5Desc2, etc.) in V2 JSON.
/// </summary>
public class InterpretativeReportDescription
{
    /// <summary>
    /// Row code identifying the type of description row.
    /// Different codes used across sections: 1, 17, 18, 27, 28, 30, 51
    /// </summary>
    public int RowCode { get; set; }

    /// <summary>
    /// Category identifier for the row.
    /// </summary>
    public int Category { get; set; }

    /// <summary>
    /// Type of row: typically FixedRow for descriptions.
    /// </summary>
    public InterpretativeReportRowType? RowType { get; set; }

    /// <summary>
    /// The descriptive text content.
    /// Maps to various value fields depending on section:
    /// - p5Desc1: value_23331
    /// - descriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod: value_2531
    /// - companyEstimatesOfFinancingProgramsAndCompanyFinanceChanges: value_2441
    /// - corporateIncomeProgram: N/A (uses AdditionalValue1-5 for numeric columns)
    /// - otherImportantPrograms: value_2471 (value_2472 in AdditionalValue1)
    /// - otherImportantNotes: value_2481
    /// - p5Desc2: value_23461
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Name of the section this description belongs to.
    /// Used to identify which part of the report this description is from.
    /// Examples: "p5Desc1", "financingDescription", "importantNotes", "p5Desc2"
    /// </summary>
    public string? SectionName { get; set; }

    /// <summary>
    /// Additional value field 1.
    /// Used for sections with multiple value columns like corporateIncomeProgram.
    /// Maps to: value_2461 (for corporateIncomeProgram)
    /// </summary>
    public string? AdditionalValue1 { get; set; }

    /// <summary>
    /// Additional value field 2.
    /// Used for sections with multiple value columns.
    /// Maps to: value_2462 (for corporateIncomeProgram)
    /// </summary>
    public string? AdditionalValue2 { get; set; }

    /// <summary>
    /// Additional value field 3.
    /// Used for sections with multiple value columns.
    /// Maps to: value_2463 (for corporateIncomeProgram)
    /// </summary>
    public string? AdditionalValue3 { get; set; }

    /// <summary>
    /// Additional value field 4.
    /// Used for sections with multiple value columns.
    /// Maps to: value_2464 (for corporateIncomeProgram)
    /// </summary>
    public string? AdditionalValue4 { get; set; }

    /// <summary>
    /// Additional value field 5.
    /// Used for sections with multiple value columns.
    /// Maps to: value_2465 (for corporateIncomeProgram)
    /// </summary>
    public string? AdditionalValue5 { get; set; }
}
