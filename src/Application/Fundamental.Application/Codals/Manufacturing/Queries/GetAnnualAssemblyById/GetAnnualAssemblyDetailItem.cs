using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblyById;

#pragma warning disable SA1402, SA1649

public sealed record GetAnnualAssemblyDetailItem(
    Guid Id,
    string Isin,
    string Symbol,
    string Title,
    string HtmlUrl,
    string Version,
    int FiscalYear,
    int YearEndMonth,
    int ReportMonth,
    DateTime AssemblyDate,
    ulong TraceNo,
    DateTime PublishDate,
    AssemblyResultType AssemblyResultType,
    string? AssemblyResultTypeTitle,
    string? AssemblyHour,
    string? AssemblyLocation,
    string? AssemblyDay,
    ulong? LetterTracingNo,
    string? AssemblyChief,
    string? AssemblySuperVisor1,
    string? AssemblySuperVisor2,
    string? AssemblySecretary,
    string? BoardMemberPeriod,
    string? PublishSecurityDescription,
    string? OtherDescription,
    string? NewHour,
    string? NewDay,
    string? NewDate,
    string? NewLocation,
    string? BreakDescription,
    List<SessionOrderDto> SessionOrders,
    List<ShareHolderDto> ShareHolders,
    List<AssemblyBoardMemberDto> AssemblyBoardMembers,
    List<InspectorDto> Inspectors,
    List<NewBoardMemberDto> NewBoardMembers,
    List<BoardMemberWageAndGiftDto> BoardMemberWageAndGifts,
    List<NewsPaperDto> NewsPapers,
    List<AssemblyInterimDto> AssemblyInterims,
    List<ProportionedRetainedEarningDto> ProportionedRetainedEarnings,
    AssemblyAttendeeDto? Ceo,
    AssemblyAttendeeDto? AuditCommitteeChairman,
    AssemblyAttendeeDto? IndependentAuditorRepresentative,
    AssemblyAttendeeDto? TopFinancialPosition
);

public sealed record SessionOrderDto(
    SessionOrderType Type,
    string? Description,
    string? FieldName
);

public sealed record ShareHolderDto(
    int? ShareHolderSerial,
    string? Name,
    long? ShareCount,
    decimal? SharePercent
);

public sealed record AssemblyBoardMemberDto(
    int? BoardMemberSerial,
    string? FullName,
    string? NationalCode,
    LegalCompanyType? LegalType,
    BoardMembershipType MembershipType,
    string? AgentBoardMemberFullName,
    string? AgentBoardMemberNationalCode,
    BoardPosition Position,
    bool HasDuty,
    string? Degree,
    int? DegreeRef,
    string? EducationField,
    int? EducationFieldRef,
    bool AttendingMeeting,
    VerificationStatus Verification
);

public sealed record InspectorDto(
    int? Serial,
    string? Name,
    InspectorType Type
);

public sealed record NewBoardMemberDto(
    string? Name,
    bool IsLegal,
    string? NationalCode,
    int? BoardMemberSerial,
    LegalCompanyType? LegalType,
    BoardMembershipType MembershipType
);

public sealed record BoardMemberWageAndGiftDto(
    WageAndGiftFieldType Type,
    string? FieldName,
    decimal? CurrentYearValue,
    decimal? PastYearValue,
    string? Description
);

public sealed record NewsPaperDto(
    int? NewsPaperId,
    string? Name
);

public sealed record AssemblyInterimDto(
    string? FieldName,
    string? Description,
    decimal? YearEndToDateValue,
    decimal? Percent,
    string? ChangesReason,
    string? RowClass
);

public sealed record ProportionedRetainedEarningDto(
    ProportionedRetainedEarningFieldName? FieldName,
    string? Description,
    decimal? YearEndToDateValue,
    string? RowClass
);

public sealed record AssemblyAttendeeDto(
    string? FullName,
    string? NationalCode,
    bool AttendingMeeting,
    string? Degree,
    int? DegreeRef,
    string? EducationField,
    int? EducationFieldRef,
    VerificationStatus? Verification
);
