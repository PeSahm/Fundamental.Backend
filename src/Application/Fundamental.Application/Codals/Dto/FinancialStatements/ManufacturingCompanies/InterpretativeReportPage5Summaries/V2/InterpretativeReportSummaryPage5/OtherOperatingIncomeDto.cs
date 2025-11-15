using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    InterpretativeReportSummaryPage5;

#pragma warning disable SA1402, SA1649

/// <summary>
/// DTO for otherOperatingIncome section (سایر درآمدهای عملیاتی).
/// </summary>
public class OtherOperatingIncomeDto
{
    [JsonProperty("yearData")]
    public List<YearDataDto>? YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<OtherOperatingIncomeRowDto>? RowItems { get; set; }
}

public class OtherOperatingIncomeRowDto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string? OldFieldName { get; set; }

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string? RowType { get; set; }

    [JsonProperty("id")]
    public string? Id { get; set; }

    [JsonProperty("value_2421")]
    public string? Value2421 { get; set; }

    [JsonProperty("value_2422")]
    public string? Value2422 { get; set; }

    [JsonProperty("value_2423")]
    public string? Value2423 { get; set; }

    [JsonProperty("value_2424")]
    public string? Value2424 { get; set; }

    public string? GetDescription() => Value2421?.NormalizePersianText(PersianNormalizers.ApplyPersianYeKe);

    public decimal GetValue(string columnId)
    {
        string? value = columnId switch
        {
            "2421" => Value2421,
            "2422" => Value2422,
            "2423" => Value2423,
            "2424" => Value2424,
            _ => null
        };

        return decimal.TryParse(value, out decimal result) ? result : 0;
    }
}