using System.Text.Json.Serialization;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V2;

public class FieldsItemV2Dto
{
    [JsonPropertyName("productName")]
    public string ProductName { get; set; } = null!;

    [JsonPropertyName("productSerial")]
    public int ProductSerial { get; set; }

    [JsonPropertyName("productUnit")]
    public string ProductUnit { get; set; } = null!;

    [JsonPropertyName("products")]
    public List<ProductV2Dto> Products { get; set; } = new();

    [JsonPropertyName("correction")]
    public CorrectionV2Dto Correction { get; set; } = null!;
}