using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// فرد شرکت‌کننده در مجمع (مدیرعامل، رئیس کمیته حسابرسی، حسابرس، مقام مالی).
/// </summary>
public class AssemblyAttendee
{
    /// <summary>
    /// نام کامل.
    /// </summary>
    public string? FullName { get; set; }

    /// <summary>
    /// کد ملی.
    /// </summary>
    public string? NationalCode { get; set; }

    /// <summary>
    /// حضور در جلسه.
    /// </summary>
    public bool AttendingMeeting { get; set; }

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
    /// وضعیت تایید صلاحیت.
    /// </summary>
    public VerificationStatus? Verification { get; set; }
}
