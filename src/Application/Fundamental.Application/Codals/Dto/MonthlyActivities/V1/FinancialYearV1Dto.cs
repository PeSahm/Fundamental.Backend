using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V1;

public class FinancialYearV1Dto
{
    [JsonPropertyName("priodEndToDate")]
    public string PriodEndToDate { get; set; } = null!;

    [JsonPropertyName("yearEndToDate")]
    public string YearEndToDate { get; set; } = null!;
}