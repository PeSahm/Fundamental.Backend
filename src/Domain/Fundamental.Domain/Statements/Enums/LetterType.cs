using System.ComponentModel;

namespace Fundamental.Domain.Statements.Enums;

/// <summary>
/// انواع اطلاعیه.
/// </summary>
public enum LetterType
{
    [Description("اطلاعات و صورت های مالی میاندوره ای")]
    InterimStatement = 6,

    [Description("گزارش فعالیت ماهانه")]
    MonthlyActivity = 58,

    [Description("نامشخص")]
    UnKnown = -1
}