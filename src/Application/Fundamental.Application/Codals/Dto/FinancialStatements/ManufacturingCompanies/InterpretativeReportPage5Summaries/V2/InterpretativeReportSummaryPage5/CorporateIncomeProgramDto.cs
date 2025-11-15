using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// DTO for corporateIncomeProgram section.
/// </summary>
public class CorporateIncomeProgramDto
{
    [JsonProperty("rowItems")]
    public List<CorporateIncomeProgramRowDto>? RowItems { get; set; }
}

public class CorporateIncomeProgramRowDto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string? OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string? RowType { get; set; }

    [JsonProperty("value_2461")]
    public string? Value2461 { get; set; }

    [JsonProperty("value_2462")]
    public string? Value2462 { get; set; }

    [JsonProperty("value_2463")]
    public string? Value2463 { get; set; }

    [JsonProperty("value_2464")]
    public string? Value2464 { get; set; }

    [JsonProperty("value_2465")]
    public string? Value2465 { get; set; }
}
