using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for share value change (تغییر ارزش اسمی).
/// </summary>
public class ShareValueChangeCapitalDto
{
    [JsonProperty("isAccept")]
    public bool? IsAccept { get; set; }

    [JsonProperty("newShareCount")]
    public long? NewShareCount { get; set; }

    [JsonProperty("newShareValue")]
    public int? NewShareValue { get; set; }
}
