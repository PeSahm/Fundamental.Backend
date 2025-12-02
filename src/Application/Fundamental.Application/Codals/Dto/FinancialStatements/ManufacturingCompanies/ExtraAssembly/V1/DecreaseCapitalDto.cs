using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for capital decrease (کاهش سرمایه).
/// </summary>
public class DecreaseCapitalDto
{
    [JsonProperty("capitalDecreaseValue")]
    public decimal? CapitalDecreaseValue { get; set; }

    [JsonProperty("decreasePercent")]
    public decimal? DecreasePercent { get; set; }

    [JsonProperty("isAccept")]
    public bool IsAccept { get; set; }

    [JsonProperty("newCapital")]
    public long? NewCapital { get; set; }

    [JsonProperty("newShareCount")]
    public long? NewShareCount { get; set; }

    [JsonProperty("newShareValue")]
    public int? NewShareValue { get; set; }
}
