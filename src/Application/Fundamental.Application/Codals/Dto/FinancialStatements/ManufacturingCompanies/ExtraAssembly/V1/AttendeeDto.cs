using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// DTO for assembly attendee (حضور افراد خاص).
/// </summary>
public class AttendeeDto
{
    [JsonProperty("attendingMeeting")]
    public int AttendingMeeting { get; set; }

    [JsonProperty("fullName")]
    public string? FullName { get; set; }

    [JsonProperty("nationalCode")]
    public string? NationalCode { get; set; }

    [JsonProperty("degreeRef")]
    public int? DegreeRef { get; set; }

    [JsonProperty("educationFieldRef")]
    public int? EducationFieldRef { get; set; }

    [JsonProperty("degree")]
    public string? Degree { get; set; }

    [JsonProperty("educationField")]
    public string? EducationField { get; set; }

    [JsonProperty("verification")]
    public string? Verification { get; set; }
}
