using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class YearDataV3Dto
{
    [JsonProperty("columnId")]
    public string ColumnId { get; set; } = null!;

    [JsonProperty("caption")]
    public string Caption { get; set; } = null!;

    [JsonProperty("periodEndToDate")]
    public string PeriodEndToDate { get; set; } = null!;

    [JsonProperty("yearEndToDate")]
    public string YearEndToDate { get; set; } = null!;

    [JsonProperty("period")]
    public int Period { get; set; }

    [JsonProperty("isAudited")]
    public bool? IsAudited { get; set; }
}