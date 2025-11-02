using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V1;

public class ProductAndSalesV1Dto
{
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = null!;

    [JsonPropertyName("productSerial")]
    public int ProductSerial { get; set; }

    [JsonPropertyName("productUnit")]
    public string ProductUnit { get; set; } = null!;

    [JsonPropertyName("totalProductionInPeriod")]
    public decimal TotalProductionInPeriod { get; set; }

    [JsonPropertyName("totalSalesInPeriod")]
    public decimal TotalSalesInPeriod { get; set; }

    [JsonPropertyName("salesAmountInPeriod")]
    public decimal SalesAmountInPeriod { get; set; }

    [JsonPropertyName("salesRateInPeriod")]
    public decimal SalesRateInPeriod { get; set; }

    [JsonPropertyName("totalProductionInYear")]
    public decimal TotalProductionInYear { get; set; }

    [JsonPropertyName("totalSalesInYear")]
    public decimal TotalSalesInYear { get; set; }

    [JsonPropertyName("salesAmountInYear")]
    public decimal SalesAmountInYear { get; set; }

    [JsonPropertyName("salesRateInYear")]
    public decimal SalesRateInYear { get; set; }
}