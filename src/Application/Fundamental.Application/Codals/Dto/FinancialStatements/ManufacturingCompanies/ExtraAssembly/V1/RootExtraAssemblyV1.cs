using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// Wrapper DTO for ExtraAssembly V1 JSON deserialization.
/// </summary>
public class RootExtraAssemblyV1
{
    [JsonProperty("decision-ExtraAssembly")]
    public CodalExtraAssemblyV1? Decision { get; set; }
}
