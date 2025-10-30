using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class ProductionAndSalesV3Dto
{
    [JsonProperty("yearData")]
    public List<YearDataV3Dto> YearData { get; set; } = new();

    [JsonProperty("rowItems")]
    public List<ProductionAndSalesRowItemV3Dto> RowItems { get; set; } = new();
}