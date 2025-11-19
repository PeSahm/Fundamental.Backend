using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for assembly chief members (اعضای هیئت رئیسه مجمع).
/// </summary>
public class AssemblyChiefMembersDto
{
    [JsonProperty("assemblyChief")]
    public string? AssemblyChief { get; set; }

    [JsonProperty("assemblySuperVisor1")]
    public string? AssemblySuperVisor1 { get; set; }

    [JsonProperty("assemblySuperVisor2")]
    public string? AssemblySuperVisor2 { get; set; }

    [JsonProperty("assemblySecretary")]
    public string? AssemblySecretary { get; set; }
}
