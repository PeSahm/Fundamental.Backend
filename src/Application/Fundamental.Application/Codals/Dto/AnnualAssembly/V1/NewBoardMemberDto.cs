using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for new board member (عضو جدید هیئت مدیره).
/// </summary>
public class NewBoardMemberDto
{
    [JsonProperty("name")]
    public string? Name { get; set; }

    [JsonProperty("isLegal")]
    public bool IsLegal { get; set; }

    [JsonProperty("legalType")]
    public int? LegalType { get; set; }

    [JsonProperty("nationalCode")]
    public string? NationalCode { get; set; }

    [JsonProperty("membershipType")]
    public int MembershipType { get; set; }

    [JsonProperty("boardMemberSerial")]
    public int BoardMemberSerial { get; set; }
}
