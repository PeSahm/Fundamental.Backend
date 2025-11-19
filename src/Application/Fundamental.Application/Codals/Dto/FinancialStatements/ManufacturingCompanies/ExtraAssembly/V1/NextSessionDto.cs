using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for next session (مجمع بعدی - اعلام تنفس).
/// </summary>
public class NextSessionDto
{
    [JsonProperty("breakDesc")]
    public string? BreakDesc { get; set; }

    [JsonProperty("hour")]
    public string? Hour { get; set; }

    [JsonProperty("date")]
    public string? Date { get; set; }

    [JsonProperty("day")]
    public string? Day { get; set; }

    [JsonProperty("location")]
    public string? Location { get; set; }
}
