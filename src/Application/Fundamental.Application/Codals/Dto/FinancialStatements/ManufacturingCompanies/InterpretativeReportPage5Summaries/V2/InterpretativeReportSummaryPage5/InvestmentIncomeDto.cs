using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// DTO for nonOperationIncomeAndExpensesInvestmentIncome section (درآمدهای سرمایه‌گذاری).
/// </summary>
public class InvestmentIncomeDto
{
    [JsonProperty("yearData")]
    public List<YearDataDto>? YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<InvestmentIncomeRowDto>? RowItems { get; set; }
}

public class InvestmentIncomeRowDto
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

    [JsonProperty("value_2451")]
    public string? Value2451 { get; set; }

    [JsonProperty("value_2452")]
    public string? Value2452 { get; set; }

    [JsonProperty("value_2453")]
    public string? Value2453 { get; set; }

    [JsonProperty("value_2454")]
    public string? Value2454 { get; set; }

    [JsonProperty("value_2455")]
    public string? Value2455 { get; set; }

    public string? GetDescription() => Value2451?.NormalizePersianText(PersianNormalizers.ApplyPersianYeKe);

    public decimal GetValue(string columnId)
    {
        string? value = columnId switch
        {
            "2451" => Value2451,
            "2452" => Value2452,
            "2453" => Value2453,
            "2454" => Value2454,
            "2455" => Value2455,
            _ => null
        };

        return decimal.TryParse(value, out decimal result) ? result : 0;
    }
}
