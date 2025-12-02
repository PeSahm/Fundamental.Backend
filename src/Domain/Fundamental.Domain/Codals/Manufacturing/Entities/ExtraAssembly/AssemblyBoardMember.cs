using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اعضای هیئت مدیره حاضر در مجمع
/// </summary>
public sealed class AssemblyBoardMember
{
    /// <summary>
    /// سریال عضو حقیقی یا حقوقی هیئت مدیره
    /// </summary>
    public int? BoardMemberSerial { get; init; }

    /// <summary>
    /// نام عضو حقیقی یا حقوقی هیئت مدیره
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// شمارۀ ثبت عضو حقوقی/کد ملی
    /// </summary>
    public string? NationalCode { get; init; }

    /// <summary>
    /// نوع شخصیت حقوقی
    /// </summary>
    public LegalCompanyType? LegalType { get; init; }

    /// <summary>
    /// نوع عضویت
    /// </summary>
    public BoardMembershipType MembershipType { get; init; }

    /// <summary>
    /// نام نماینده عضو حقوقی
    /// </summary>
    public string? AgentBoardMemberFullName { get; init; }

    /// <summary>
    /// کد ملی نماینده عضو حقوقی
    /// </summary>
    public string? AgentBoardMemberNationalCode { get; init; }

    /// <summary>
    /// سمت در هیئت مدیره
    /// </summary>
    public BoardPosition Position { get; init; }

    /// <summary>
    /// موظف/غیر موظف
    /// </summary>
    public bool HasDuty { get; init; }

    /// <summary>
    /// مقطع تحصیلی
    /// </summary>
    public string? Degree { get; init; }

    /// <summary>
    /// سریال مقطع تحصیلی
    /// </summary>
    public int? DegreeRef { get; init; }

    /// <summary>
    /// رشته ی تحصیلی
    /// </summary>
    public string? EducationField { get; init; }

    /// <summary>
    /// سریال رشته ی تحصیلی
    /// </summary>
    public int? EducationFieldRef { get; init; }

    /// <summary>
    /// آیا در جلسه حضور داشته؟
    /// </summary>
    public bool AttendingMeeting { get; init; }

    /// <summary>
    /// تایید صلاحیت
    /// </summary>
    public VerificationStatus Verification { get; init; }
}
