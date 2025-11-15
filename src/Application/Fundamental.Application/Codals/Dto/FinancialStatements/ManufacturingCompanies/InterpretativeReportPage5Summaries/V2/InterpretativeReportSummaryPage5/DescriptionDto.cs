using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// DTO for various description sections (p5Desc1, p5Desc2, etc.).
/// </summary>
public class DescriptionDto
{
    [JsonProperty("rowItems")]
    public List<DescriptionRowDto>? RowItems { get; set; }
}

public class DescriptionRowDto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string? OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string? RowType { get; set; }

    // p5Desc1
    [JsonProperty("value_23331")]
    public string? Value23331 { get; set; }

    // descriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod
    [JsonProperty("value_2531")]
    public string? Value2531 { get; set; }

    // companyEstimatesOfFinancingProgramsAndCompanyFinanceChanges
    [JsonProperty("value_2441")]
    public string? Value2441 { get; set; }

    // otherImportantNotes
    [JsonProperty("value_2481")]
    public string? Value2481 { get; set; }

    // p5Desc2
    [JsonProperty("value_23341")]
    public string? Value23341 { get; set; }
}
