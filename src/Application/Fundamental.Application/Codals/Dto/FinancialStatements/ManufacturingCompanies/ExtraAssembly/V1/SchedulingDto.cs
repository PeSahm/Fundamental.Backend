using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for scheduling (زمانبندی).
/// </summary>
public class SchedulingDto
{
    [JsonProperty("isRegistered")]
    public bool IsRegistered { get; set; }

    [JsonProperty("yearEndToDate")]
    public string? YearEndToDate { get; set; }
}
