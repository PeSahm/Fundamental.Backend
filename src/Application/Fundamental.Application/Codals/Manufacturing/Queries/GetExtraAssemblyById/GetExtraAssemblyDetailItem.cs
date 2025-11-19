using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;

#pragma warning disable SA1402, SA1649

public sealed record GetExtraAssemblyDetailItem(
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
    string? AssemblyDate_ParentInfo,
    string? AssemblyHour,
    string? AssemblyLocation,
    string? AssemblyDay,
    string? LetterPublishDate,
    int? LetterTracingNo,
    string? AssemblyChief,
    string? AssemblySuperVisor1,
    string? AssemblySuperVisor2,
    string? AssemblySecretary,
    CapitalChangeState CapitalChangeState,
    int? LastShareValue,
    int? LastCapital,
    long? LastShareCount,
    string? OldAddress,
    string? NewAddress,
    string? OldName,
    string? NewName,
    string? OldActivitySubject,
    string? NewActivitySubject,
    int? OldFinancialYearMonthLength,
    string? OldFinancialYearEndDate,
    int? OldFinancialYearDayLength,
    string? NewFinancialYearEndDate,
    string? NewFinancialYearMonthLength,
    string? NewFinancialYearDayLength,
    bool IsLocationChange,
    bool IsNameChange,
    bool IsActivitySubjectChange,
    bool IsFinancialYearChange,
    bool IsDecidedClause141,
    string? DecidedClause141Des,
    bool IsAccordWithSEOStatuteApproved,
    string? OtherDes,
    int? PrimaryMarketTracingNo,
    bool CorrectionStatuteApproved,
    string? NextSessionBreakDesc,
    string? NextSessionHour,
    string? NextSessionDate,
    string? NextSessionDay,
    string? NextSessionLocation,
    bool? SchedulingIsRegistered,
    string? SchedulingYearEndToDate,
    ExtraAssemblyDecreaseCapitalQueryDto? DecreaseCapital,
    ExtraAssemblyShareValueChangeCapitalQueryDto? ShareValueChangeCapital,
    List<SessionOrderQueryDto> SessionOrders,
    List<ShareHolderQueryDto> ShareHolders,
    List<AssemblyBoardMemberQueryDto> AssemblyBoardMembers,
    List<ExtraAssemblyIncreaseCapitalQueryDto> IncreaseCapitals,
    AssemblyAttendeeQueryDto? Ceo,
    AssemblyAttendeeQueryDto? AuditCommitteeChairman
);

public sealed record SessionOrderQueryDto(
    SessionOrderType Type,
    string? Description,
    string? FieldName
);

public sealed record ShareHolderQueryDto(
    int? ShareHolderSerial,
    string? Name,
    long? ShareCount,
    double? SharePercent
);

public sealed record AssemblyBoardMemberQueryDto(
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

public sealed record ExtraAssemblyIncreaseCapitalQueryDto(
    int? CashIncoming,
    int? RetainedEarning,
    int? Reserves,
    int? RevaluationSurplus,
    int? SarfSaham,
    bool IsAccept,
    int? CapitalIncreaseValue,
    double? IncreasePercent,
    CapitalIncreaseApprovalType Type,
    decimal? CashForceclosurePriorityStockPrice,
    string? CashForceclosurePriorityStockDesc,
    int? CashForceclosurePriorityAvalableStockCount,
    int? CashForceclosurePriorityPrizeStockCount,
    decimal? CashForceclosurePriority
);

public sealed record ExtraAssemblyDecreaseCapitalQueryDto(
    long? CapitalDecreaseValue,
    long? DecreasePercent,
    bool IsAccept,
    long? NewCapital,
    long? NewShareCount,
    int? NewShareValue
);

public sealed record ExtraAssemblyShareValueChangeCapitalQueryDto(
    bool IsAccept,
    long? NewShareCount,
    int? NewShareValue
);

public sealed record AssemblyAttendeeQueryDto(
    string? FullName,
    string? NationalCode,
    bool AttendingMeeting,
    string? Degree,
    int? DegreeRef,
    string? EducationField,
    int? EducationFieldRef,
    VerificationStatus? Verification
);
