using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class FinancialYearV2Dto
{
    [JsonPropertyName("prevPeriodEndToDate")]
    public string PrevPeriodEndToDate { get; set; } = null!;

    [JsonPropertyName("priodEndToDate")]
    public string PriodEndToDate { get; set; } = null!;
}