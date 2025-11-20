using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for parent assembly (مجمع اصلی).
/// </summary>
public class ParentAssemblyDto
{
    [JsonProperty("assemblyResultType")]
    public int AssemblyResultType { get; set; }

    [JsonProperty("assemblyResultTypeTitle")]
    public string? AssemblyResultTypeTitle { get; set; }

    [JsonProperty("date")]
    public string? Date { get; set; }

    [JsonProperty("hour")]
    public string? Hour { get; set; }

    [JsonProperty("location")]
    public string? Location { get; set; }

    [JsonProperty("day")]
    public string? Day { get; set; }

    [JsonProperty("letterPublishDate")]
    public string? LetterPublishDate { get; set; }

    [JsonProperty("letterTracingNo")]
    public int? LetterTracingNo { get; set; }

    [JsonProperty("sessionOrders")]
    public List<SessionOrderDto>? SessionOrders { get; set; }
}
