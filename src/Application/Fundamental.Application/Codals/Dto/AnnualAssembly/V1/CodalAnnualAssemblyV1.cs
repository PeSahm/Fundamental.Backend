using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.AnnualAssembly.V1;

/// <summary>
/// Root DTO for Annual Assembly V1 (تصمیمات مجمع عمومی عادی سالیانه).
/// </summary>
public class CodalAnnualAssemblyV1 : ICodalMappingServiceMetadata
{
    public ReportingType ReportingType => ReportingType.Production;

    public LetterType LetterType => LetterType.AnnualGeneralMeetingDecisions;

    public CodalVersion CodalVersion => CodalVersion.V1;

    public LetterPart LetterPart => LetterPart.AnnualGeneralMeetingDecisions;

    [JsonProperty("parentAssembly")]
    public ParentAssemblyDto? ParentAssembly { get; set; }

    [JsonProperty("shareHolders")]
    public List<ShareHolderDto>? ShareHolders { get; set; }

    [JsonProperty("assemblyChiefMembers")]
    public AssemblyChiefMembersDto? AssemblyChiefMembers { get; set; }

    [JsonProperty("assemblyBoardMembers")]
    public List<AssemblyBoardMemberDto>? AssemblyBoardMembers { get; set; }

    [JsonProperty("ceo")]
    public AttendeeDto? Ceo { get; set; }

    [JsonProperty("auditCommitteeChairman")]
    public AttendeeDto? AuditCommitteeChairman { get; set; }

    [JsonProperty("independentAuditorLegalInspectorRepresentative")]
    public AttendeeDto? IndependentAuditorRepresentative { get; set; }

    [JsonProperty("topFinancialPosition")]
    public AttendeeDto? TopFinancialPosition { get; set; }

    [JsonProperty("assemblyInterims")]
    public List<InterimDto>? AssemblyInterims { get; set; }

    [JsonProperty("assemblyProportionedRetainedEarning")]
    public List<RetainedEarningDto>? AssemblyProportionedRetainedEarning { get; set; }

    [JsonProperty("inspectors")]
    public List<InspectorDto>? Inspectors { get; set; }

    [JsonProperty("newBoardMembers")]
    public List<NewBoardMemberDto>? NewBoardMembers { get; set; }

    [JsonProperty("boardMemberWageAndGifts")]
    public List<WageAndGiftDto>? BoardMemberWageAndGifts { get; set; }

    [JsonProperty("newsPapers")]
    public List<NewsPaperDto>? NewsPapers { get; set; }

    [JsonProperty("boardMemberPeriod")]
    public int? BoardMemberPeriod { get; set; }

    [JsonProperty("publishSecurityDes")]
    public string? PublishSecurityDescription { get; set; }

    [JsonProperty("otherDescription")]
    public string? OtherDescription { get; set; }

    [JsonProperty("newHour")]
    public string? NewHour { get; set; }

    [JsonProperty("newDay")]
    public string? NewDay { get; set; }

    [JsonProperty("newDate")]
    public string? NewDate { get; set; }

    [JsonProperty("newLocation")]
    public string? NewLocation { get; set; }

    [JsonProperty("breakDes")]
    public string? BreakDescription { get; set; }

    /// <summary>
    /// Validates if this is a valid annual assembly report.
    /// </summary>
    public bool IsValidReport()
    {
        return ParentAssembly != null;
    }
}
