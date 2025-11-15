namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// نوع فیلد حق حضور و پاداش.
/// </summary>
public enum WageAndGiftFieldType
{
    /// <summary>
    /// حق حضور.
    /// </summary>
    Wage = 0,

    /// <summary>
    /// پاداش.
    /// </summary>
    Gift = 1,

    /// <summary>
    /// حق حضور اعضای هیات مدیره عضو کمیته حسابرسی.
    /// </summary>
    AuditCommitteeWage = 2,

    /// <summary>
    /// حق حضور اعضای هیات مدیره عضو کمیته انتصابات.
    /// </summary>
    AppointmentCommitteeWage = 3,

    /// <summary>
    /// سایر کمیته های تخصصی.
    /// </summary>
    OtherCommittees = 4,

    /// <summary>
    /// هزینه های مسولیت اجتماعی.
    /// </summary>
    SocialResponsibilityExpenses = 5
}
