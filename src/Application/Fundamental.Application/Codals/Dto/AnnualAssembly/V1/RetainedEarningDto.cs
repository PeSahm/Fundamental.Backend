using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for proportioned retained earning (سود انباشته).
/// </summary>
public class RetainedEarningDto
{
    [JsonProperty("fieldName")]
    public string? FieldName { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("yearEndToDateValue")]
    public long? YearEndToDateValue { get; set; }

    [JsonProperty("rowClass")]
    public string? RowClass { get; set; }
}
