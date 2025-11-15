using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for inspector (حسابرس/بازرس).
/// </summary>
public class InspectorDto
{
    [JsonProperty("serial")]
    public int Serial { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("type")]
    public int Type { get; set; }
}
