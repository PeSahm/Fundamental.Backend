using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// DTO for assembly board member (عضو هیئت مدیره حاضر در مجمع).
/// </summary>
public class AssemblyBoardMemberDto
{
    [JsonProperty("boardMemberSerial")]
    public int BoardMemberSerial { get; set; }

    [JsonProperty("fullName")]
    public string? FullName { get; set; }

    [JsonProperty("nationalCode")]
    public string? NationalCode { get; set; }

    [JsonProperty("legalType")]
    public int? LegalType { get; set; }

    [JsonProperty("membershipType")]
    public int MembershipType { get; set; }

    [JsonProperty("agentBoardMemberFullName")]
    public string? AgentBoardMemberFullName { get; set; }

    [JsonProperty("agentBoardMemberNationalCode")]
    public string? AgentBoardMemberNationalCode { get; set; }

    [JsonProperty("position")]
    public int Position { get; set; }

    [JsonProperty("hasDuty")]
    public int HasDuty { get; set; }

    [JsonProperty("degree")]
    public string? Degree { get; set; }

    [JsonProperty("degreeRef")]
    public int? DegreeRef { get; set; }

    [JsonProperty("educationField")]
    public string? EducationField { get; set; }

    [JsonProperty("educationFieldRef")]
    public int? EducationFieldRef { get; set; }

    [JsonProperty("attendingMeeting")]
    public bool AttendingMeeting { get; set; }

    [JsonProperty("verification")]
    public int Verification { get; set; }
}
