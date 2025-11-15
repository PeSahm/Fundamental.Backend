using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// عضو هیئت مدیره حاضر در مجمع.
/// </summary>
public class AssemblyBoardMember
{
    /// <summary>
    /// سریال عضو.
    /// </summary>
    public int BoardMemberSerial { get; set; }

    /// <summary>
    /// نام کامل عضو حقیقی یا حقوقی.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// شماره ثبت عضو حقوقی یا کد ملی عضو حقیقی.
    /// </summary>
    public string? NationalCode { get; set; }

    /// <summary>
    /// نوع شرکت (برای اعضای حقوقی).
    /// </summary>
    public LegalCompanyType? LegalType { get; set; }

    /// <summary>
    /// نوع عضویت (اصلی/علی البدل).
    /// </summary>
    public BoardMembershipType MembershipType { get; set; }

    /// <summary>
    /// نام نماینده عضو حقوقی.
    /// </summary>
    public string? AgentBoardMemberFullName { get; set; }

    /// <summary>
    /// کد ملی نماینده عضو حقوقی.
    /// </summary>
    public string? AgentBoardMemberNationalCode { get; set; }

    /// <summary>
    /// سمت در هیئت مدیره.
    /// </summary>
    public BoardPosition Position { get; set; }

    /// <summary>
    /// موظف/غیر موظف.
    /// </summary>
    public bool HasDuty { get; set; }

    /// <summary>
    /// مقطع تحصیلی.
    /// </summary>
    public string? Degree { get; set; }

    /// <summary>
    /// سریال مقطع تحصیلی.
    /// </summary>
    public int? DegreeRef { get; set; }

    /// <summary>
    /// رشته تحصیلی.
    /// </summary>
    public string? EducationField { get; set; }

    /// <summary>
    /// سریال رشته تحصیلی.
    /// </summary>
    public int? EducationFieldRef { get; set; }

    /// <summary>
    /// حضور در جلسه.
    /// </summary>
    public bool AttendingMeeting { get; set; }

    /// <summary>
    /// وضعیت تایید صلاحیت.
    /// </summary>
    public VerificationStatus Verification { get; set; }
}
