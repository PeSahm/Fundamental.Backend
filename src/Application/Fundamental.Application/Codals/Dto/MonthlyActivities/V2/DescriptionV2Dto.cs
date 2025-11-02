using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class DescriptionV2Dto
{
    // V2 descriptions appear to be empty in the sample, but keeping structure for completeness
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    [JsonPropertyName("rowCode")]
    public string? RowCode { get; set; }
}