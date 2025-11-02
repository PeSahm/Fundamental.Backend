using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class ProductV2Dto
{
    [JsonPropertyName("typeId")]
    public int TypeId { get; set; }

    [JsonPropertyName("totalProduction")]
    public decimal TotalProduction { get; set; }

    [JsonPropertyName("totalSales")]
    public decimal TotalSales { get; set; }

    [JsonPropertyName("salesRate")]
    public decimal SalesRate { get; set; }

    [JsonPropertyName("salesAmount")]
    public decimal SalesAmount { get; set; }
}