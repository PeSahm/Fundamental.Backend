using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات حضور افراد خاص (مدیرعامل، رئیس کمیته حسابرسی، نماینده حسابرس، بالاترین مقام مالی)
/// </summary>
public sealed class AssemblyAttendee
{
    /// <summary>
    /// نام و نام خانوادگی
    /// </summary>
    public string? FullName { get; init; }

    /// <summary>
    /// کد ملی
    /// </summary>
    public string? NationalCode { get; init; }

    /// <summary>
    /// آیا در جلسه حضور داشته؟
    /// </summary>
    public bool AttendingMeeting { get; init; }

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
    /// تایید صلاحیت
    /// </summary>
    public VerificationStatus? Verification { get; init; }
}
