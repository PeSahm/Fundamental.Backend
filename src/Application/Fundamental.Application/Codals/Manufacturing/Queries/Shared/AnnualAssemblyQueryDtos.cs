using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.Shared;

#pragma warning disable SA1402, SA1649

public sealed record SessionOrderQueryDto(
    SessionOrderType Type,
    string? Description,
    string? FieldName
);

public sealed record ShareHolderQueryDto(
    int ShareHolderSerial,
    string? Name,
    long ShareCount,
    decimal? SharePercent
);

public sealed record AssemblyBoardMemberQueryDto(
    int BoardMemberSerial,
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

public sealed record InspectorQueryDto(
    int Serial,
    string? Name,
    InspectorType Type
);

public sealed record NewBoardMemberQueryDto(
    string? Name,
    bool IsLegal,
    string? NationalCode,
    int BoardMemberSerial,
    LegalCompanyType? LegalType,
    BoardMembershipType MembershipType
);

public sealed record BoardMemberWageAndGiftQueryDto(
    WageAndGiftFieldType Type,
    string? FieldName,
    decimal? CurrentYearValue,
    decimal? PastYearValue,
    string? Description
);

public sealed record NewsPaperQueryDto(
    int NewsPaperId,
    string? Name
);

public sealed record AssemblyInterimQueryDto(
    string? FieldName,
    string? Description,
    decimal? YearEndToDateValue,
    decimal? Percent,
    string? ChangesReason,
    string? RowClass
);

public sealed record ProportionedRetainedEarningQueryDto(
    ProportionedRetainedEarningFieldName? FieldName,
    string? Description,
    decimal? YearEndToDateValue,
    string? RowClass
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
