using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class ProductMonthlyActivityDesc1V3Dto
{
    [JsonProperty("rowItems")]
    public List<DescriptionRowItemV3Dto> RowItems { get; set; } = new();
}