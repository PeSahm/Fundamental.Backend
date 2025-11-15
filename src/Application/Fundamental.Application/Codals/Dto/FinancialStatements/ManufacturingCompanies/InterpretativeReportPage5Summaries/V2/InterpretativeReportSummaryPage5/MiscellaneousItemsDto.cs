using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// DTO for nonOperationIncomeAndExpensesMiscellaneousItems section (اقلام متفرقه).
/// </summary>
public class MiscellaneousItemsDto
{
    [JsonProperty("yearData")]
    public List<YearDataDto>? YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<MiscellaneousItemRowDto>? RowItems { get; set; }
}

public class MiscellaneousItemRowDto
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

    [JsonProperty("value_3531")]
    public string? Value3531 { get; set; }

    [JsonProperty("value_3532")]
    public string? Value3532 { get; set; }

    [JsonProperty("value_3533")]
    public string? Value3533 { get; set; }

    [JsonProperty("value_3534")]
    public string? Value3534 { get; set; }

    public string? GetDescription() => Value3531?.NormalizePersianText(PersianNormalizers.ApplyPersianYeKe);

    public decimal GetValue(string columnId)
    {
        string? value = columnId switch
        {
            "3531" => Value3531,
            "3532" => Value3532,
            "3533" => Value3533,
            "3534" => Value3534,
            _ => null
        };

        return decimal.TryParse(value, out decimal result) ? result : 0;
    }
}
