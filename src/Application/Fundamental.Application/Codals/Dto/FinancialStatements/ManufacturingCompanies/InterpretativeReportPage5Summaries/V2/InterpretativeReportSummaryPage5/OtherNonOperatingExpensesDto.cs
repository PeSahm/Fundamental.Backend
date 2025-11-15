using DNTPersianUtils.Core;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;

/// <summary>
/// DTO for otherNonOperatingExpenses section (سایر هزینه‌های غیرعملیاتی).
/// </summary>
public class OtherNonOperatingExpensesDto
{
    [JsonProperty("yearData")]
    public List<YearDataDto>? YearData { get; set; }

    [JsonProperty("rowItems")]
    public List<OtherNonOperatingExpenseRowDto>? RowItems { get; set; }
}

public class OtherNonOperatingExpenseRowDto
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

    [JsonProperty("value_3521")]
    public string? Value3521 { get; set; }

    [JsonProperty("value_3522")]
    public string? Value3522 { get; set; }

    [JsonProperty("value_3523")]
    public string? Value3523 { get; set; }

    [JsonProperty("value_3524")]
    public string? Value3524 { get; set; }

    public string? GetDescription() => Value3521?.NormalizePersianText(PersianNormalizers.ApplyPersianYeKe);

    public decimal GetValue(string columnId)
    {
        string? value = columnId switch
        {
            "3521" => Value3521,
            "3522" => Value3522,
            "3523" => Value3523,
            "3524" => Value3524,
            _ => null
        };

        return decimal.TryParse(value, out decimal result) ? result : 0;
    }
}
