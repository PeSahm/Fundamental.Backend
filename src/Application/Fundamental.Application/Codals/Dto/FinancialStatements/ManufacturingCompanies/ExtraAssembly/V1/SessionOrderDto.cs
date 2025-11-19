using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for session order (دستور جلسه).
/// </summary>
public class SessionOrderDto
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("fieldName")]
    public string? FieldName { get; set; }
}
