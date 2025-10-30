using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V1;

public class DescriptionV1Dto
{
    [JsonPropertyName("periodEndToDateDescription")]
    public string PeriodEndToDateDescription { get; set; } = null!;

    [JsonPropertyName("yearEndToDateDescription")]
    public string YearEndToDateDescription { get; set; } = null!;
}