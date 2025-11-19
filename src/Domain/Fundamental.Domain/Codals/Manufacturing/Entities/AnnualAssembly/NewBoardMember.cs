using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// عضو جدید هیئت مدیره.
/// </summary>
public class NewBoardMember
{
    /// <summary>
    /// نام عضو حقیقی یا حقوقی.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// ماهیت (حقیقی/حقوقی).
    /// </summary>
    public bool IsLegal { get; set; }

    /// <summary>
    /// شماره ثبت عضو حقوقی یا کد ملی عضو حقیقی.
    /// </summary>
    public string? NationalCode { get; set; }

    /// <summary>
    /// سریال عضو.
    /// </summary>
    public int BoardMemberSerial { get; set; }

    /// <summary>
    /// نوع شرکت (برای اعضای حقوقی).
    /// </summary>
    public LegalCompanyType? LegalType { get; set; }

    /// <summary>
    /// نوع عضویت (اصلی/علی البدل).
    /// </summary>
    public BoardMembershipType MembershipType { get; set; }
}
