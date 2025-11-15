using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    InterpretativeReportSummaryPage5;

#pragma warning disable SA1402, SA1649

/// <summary>
/// DTO for otherImportantPrograms section.
/// </summary>
public class OtherImportantProgramsDto
{
    [JsonProperty("rowItems")]
    public List<OtherImportantProgramRowDto>? RowItems { get; set; }
}

public class OtherImportantProgramRowDto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string? OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string? RowType { get; set; }

    [JsonProperty("value_2471")]
    public string? Value2471 { get; set; }

    [JsonProperty("value_2472")]
    public string? Value2472 { get; set; }
}