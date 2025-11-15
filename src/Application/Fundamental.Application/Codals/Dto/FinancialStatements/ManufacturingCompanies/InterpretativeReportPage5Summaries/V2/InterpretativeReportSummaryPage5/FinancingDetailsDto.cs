using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// DTO for financing details sections.
/// Maps to detailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod and detailsOfTheFinancingOfTheCompany-Est1.
/// </summary>
public class FinancingDetailsDto
{
    [JsonProperty("yearData")]
    public List<YearDataDto>? YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<FinancingDetailRowDto>? RowItems { get; set; }
}

public class FinancingDetailRowDto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string? OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string? RowType { get; set; }

    // Current period columns (2431-24311)
    [JsonProperty("value_2431")]
    public string? Value2431 { get; set; }

    [JsonProperty("value_2432")]
    public string? Value2432 { get; set; }

    [JsonProperty("value_2433")]
    public string? Value2433 { get; set; }

    [JsonProperty("value_2434")]
    public string? Value2434 { get; set; }

    [JsonProperty("value_2435")]
    public string? Value2435 { get; set; }

    [JsonProperty("value_2436")]
    public string? Value2436 { get; set; }

    [JsonProperty("value_2437")]
    public string? Value2437 { get; set; }

    [JsonProperty("value_2438")]
    public string? Value2438 { get; set; }

    [JsonProperty("value_2439")]
    public string? Value2439 { get; set; }

    [JsonProperty("value_24310")]
    public string? Value24310 { get; set; }

    [JsonProperty("value_24311")]
    public string? Value24311 { get; set; }

    // Estimated period columns (23371-233711)
    [JsonProperty("value_23371")]
    public string? Value23371 { get; set; }

    [JsonProperty("value_23372")]
    public string? Value23372 { get; set; }

    [JsonProperty("value_23373")]
    public string? Value23373 { get; set; }

    [JsonProperty("value_23374")]
    public string? Value23374 { get; set; }

    [JsonProperty("value_23375")]
    public string? Value23375 { get; set; }

    [JsonProperty("value_23376")]
    public string? Value23376 { get; set; }

    [JsonProperty("value_23377")]
    public string? Value23377 { get; set; }

    [JsonProperty("value_23378")]
    public string? Value23378 { get; set; }

    [JsonProperty("value_23379")]
    public string? Value23379 { get; set; }

    [JsonProperty("value_233710")]
    public string? Value233710 { get; set; }

    [JsonProperty("value_233711")]
    public string? Value233711 { get; set; }
}
