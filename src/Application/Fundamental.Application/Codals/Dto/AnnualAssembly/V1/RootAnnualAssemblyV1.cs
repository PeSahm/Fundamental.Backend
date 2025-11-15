using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// Wrapper DTO for Annual Assembly V1 JSON root.
/// </summary>
public class RootAnnualAssemblyV1
{
    [JsonProperty("decision-AnnualAssemblyStatement")]
    public CodalAnnualAssemblyV1? DecisionAnnualAssemblyStatement { get; set; }
}
