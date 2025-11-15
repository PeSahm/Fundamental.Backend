using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for assembly interim (اطلاعات میان دوره‌ای).
/// </summary>
public class InterimDto
{
    [JsonProperty("fieldName")]
    public string? FieldName { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("yearEndToDateValue")]
    public long? YearEndToDateValue { get; set; }

    [JsonProperty("percent")]
    public decimal? Percent { get; set; }

    [JsonProperty("changesReason")]
    public string? ChangesReason { get; set; }

    [JsonProperty("rowClass")]
    public string? RowClass { get; set; }
}
