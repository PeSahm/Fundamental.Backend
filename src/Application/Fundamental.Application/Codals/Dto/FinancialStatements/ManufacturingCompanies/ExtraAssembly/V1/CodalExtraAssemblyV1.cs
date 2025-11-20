using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;

/// <summary>
/// Root DTO for ExtraAssembly V1 (تصمیمات مجمع عمومی فوق‌العاده).
/// </summary>
public class CodalExtraAssemblyV1 : ICodalMappingServiceMetadata
{
    public ReportingType ReportingType => ReportingType.Production;

    public LetterType LetterType => LetterType.ExtraordinaryGeneralMeetingDecisions;

    public CodalVersion CodalVersion => CodalVersion.V1;

    public LetterPart LetterPart => LetterPart.NotSpecified;

    [JsonProperty("parentAssembly")]
    public ParentAssemblyDto? ParentAssembly { get; set; }

    [JsonProperty("nextSession")]
    public NextSessionDto? NextSession { get; set; }

    [JsonProperty("shareHolders")]
    public List<ShareHolderDto>? ShareHolders { get; set; }

    [JsonProperty("assemblyBoardMembers")]
    public List<AssemblyBoardMemberDto>? AssemblyBoardMembers { get; set; }

    [JsonProperty("assemblyChiefMembers")]
    public AssemblyChiefMembersDto? AssemblyChiefMembers { get; set; }

    [JsonProperty("capitalChangeState")]
    public int? CapitalChangeState { get; set; }

    [JsonProperty("ceo")]
    public AttendeeDto? Ceo { get; set; }

    [JsonProperty("auditCommitteeChairman")]
    public AttendeeDto? AuditCommitteeChairman { get; set; }

    [JsonProperty("lastshareValue")]
    public int? LastShareValue { get; set; }

    [JsonProperty("lastCapital")]
    public int? LastCapital { get; set; }

    [JsonProperty("lastshareCount")]
    public long? LastShareCount { get; set; }

    [JsonProperty("extraAssemblyIncreaseCapitals")]
    public List<IncreaseCapitalDto>? ExtraAssemblyIncreaseCapitals { get; set; }

    [JsonProperty("extraAssemblyDecreaseCapital")]
    public DecreaseCapitalDto? ExtraAssemblyDecreaseCapital { get; set; }

    [JsonProperty("extraAssemblyShareValueChangeCapitals")]
    public ShareValueChangeCapitalDto? ExtraAssemblyShareValueChangeCapitals { get; set; }

    [JsonProperty("extraAssemblyScheduling")]
    public SchedulingDto? ExtraAssemblyScheduling { get; set; }

    [JsonProperty("oldAddress")]
    public string? OldAddress { get; set; }

    [JsonProperty("newAddress")]
    public string? NewAddress { get; set; }

    [JsonProperty("oldName")]
    public string? OldName { get; set; }

    [JsonProperty("newName")]
    public string? NewName { get; set; }

    [JsonProperty("oldActivitySubject")]
    public string? OldActivitySubject { get; set; }

    [JsonProperty("newActivitySubject")]
    public string? NewActivitySubject { get; set; }

    [JsonProperty("oldFinancialYearMonthLenght")]
    public int? OldFinancialYearMonthLength { get; set; }

    [JsonProperty("oldFinancialYearEndDate")]
    public string? OldFinancialYearEndDate { get; set; }

    [JsonProperty("oldFinancialYearDayLenght")]
    public int? OldFinancialYearDayLength { get; set; }

    [JsonProperty("newFinancialYearEndDate")]
    public string? NewFinancialYearEndDate { get; set; }

    [JsonProperty("newFinancialYearMonthLenght")]
    public string? NewFinancialYearMonthLength { get; set; }

    [JsonProperty("newFinancialYearDayLenght")]
    public string? NewFinancialYearDayLength { get; set; }

    [JsonProperty("isLocationChange")]
    public bool IsLocationChange { get; set; }

    [JsonProperty("isNameChange")]
    public bool IsNameChange { get; set; }

    [JsonProperty("isActivitySubjectChange")]
    public bool IsActivitySubjectChange { get; set; }

    [JsonProperty("isFinancialYearChange")]
    public bool IsFinancialYearChange { get; set; }

    [JsonProperty("isDecidedClause141")]
    public bool IsDecidedClause141 { get; set; }

    [JsonProperty("decidedClause141Des")]
    public string? DecidedClause141Des { get; set; }

    [JsonProperty("isAccordWithSEOStatuteApproved")]
    public bool IsAccordWithSEOStatuteApproved { get; set; }

    [JsonProperty("otherDes")]
    public string? OtherDes { get; set; }

    [JsonProperty("primaryMarketTracingNo")]
    public int? PrimaryMarketTracingNo { get; set; }

    [JsonProperty("correctionStatuteApproved")]
    public bool CorrectionStatuteApproved { get; set; }

    /// <summary>
    /// Validates if this is a complete report.
    /// </summary>
    public bool IsValidReport()
    {
        return ParentAssembly != null;
    }
}
