using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

#pragma warning disable SA1402, SA1649

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
    public long? LetterTracingNo { get; set; }

    [JsonProperty("sessionOrders")]
    public List<SessionOrderDto>? SessionOrders { get; set; }
}

public class SessionOrderDto
{
    [JsonProperty("type")]
    public int Type { get; set; }

    [JsonProperty("description")]
    public string? Description { get; set; }

    [JsonProperty("fieldName")]
    public string? FieldName { get; set; }
}
