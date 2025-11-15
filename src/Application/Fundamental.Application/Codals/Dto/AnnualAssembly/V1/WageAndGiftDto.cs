using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for board member wage and gift (حق حضور و پاداش).
/// </summary>
public class WageAndGiftDto
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("fieldName")]
    public string? FieldName { get; set; }

    [JsonProperty("currentYearValue")]
    public long? CurrentYearValue { get; set; }

    [JsonProperty("pastYearValue")]
    public long? PastYearValue { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }
}
