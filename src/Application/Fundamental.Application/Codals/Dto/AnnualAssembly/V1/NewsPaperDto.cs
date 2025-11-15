using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for newspaper (روزنامه).
/// </summary>
public class NewsPaperDto
{
    [JsonProperty("newsPaperId")]
    public int NewsPaperId { get; set; }

    [JsonProperty("name")]
    public string? Name { get; set; }
}
