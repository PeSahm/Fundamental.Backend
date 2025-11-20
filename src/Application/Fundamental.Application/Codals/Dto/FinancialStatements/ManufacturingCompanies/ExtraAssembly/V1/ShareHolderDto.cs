using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for shareholder (سهامدار).
/// </summary>
public class ShareHolderDto
{
    [JsonProperty("shareHolderSerial")]
    public int? ShareHolderSerial { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("shareCount")]
    public long? ShareCount { get; set; }

    [JsonProperty("sharePercent")]
    public double? SharePercent { get; set; }
}
