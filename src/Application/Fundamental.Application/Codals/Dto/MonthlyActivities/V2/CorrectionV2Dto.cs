using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class CorrectionV2Dto
{
    [JsonPropertyName("totalProduction")]
    public decimal TotalProduction { get; set; }

    [JsonPropertyName("totalSales")]
    public decimal TotalSales { get; set; }

    [JsonPropertyName("salesAmount")]
    public decimal SalesAmount { get; set; }
}