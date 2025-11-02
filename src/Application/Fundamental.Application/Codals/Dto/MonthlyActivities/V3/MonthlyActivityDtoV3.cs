using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V3;

public class MonthlyActivityDtoV3
{
    [JsonProperty("version")]
    public string Version { get; set; } = null!;

    [JsonProperty("productionAndSales")]
    public ProductionAndSalesV3Dto ProductionAndSales { get; set; } = null!;

    [JsonProperty("productMonthlyActivityDesc1")]
    public ProductMonthlyActivityDesc1V3Dto ProductMonthlyActivityDesc1 { get; set; } = null!;
}