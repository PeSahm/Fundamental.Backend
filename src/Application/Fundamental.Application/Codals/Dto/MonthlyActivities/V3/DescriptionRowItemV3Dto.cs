using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class DescriptionRowItemV3Dto
{
    [JsonProperty("rowCode")]
    public int RowCode { get; set; }

    [JsonProperty("oldFieldName")]
    public string OldFieldName { get; set; } = string.Empty;

    [JsonProperty("category")]
    public int Category { get; set; }

    [JsonProperty("rowType")]
    public string RowType { get; set; } = null!;

    [JsonProperty("value_11991")]
    public string? Value11991 { get; set; }
}