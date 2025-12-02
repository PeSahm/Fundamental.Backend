namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// وضعیت تغییر سرمایه در مجمع عمومی فوق‌العاده
/// </summary>
public enum CapitalChangeState
{
    /// <summary>
    /// بدون تغییر سرمایه
    /// </summary>
    None = 0,

    /// <summary>
    /// تصمیم‌گیری در خصوص افزایش سرمایه
    /// </summary>
    CapitalIncrease = 1,

    /// <summary>
    /// تصمیم‌گیری در خصوص کاهش سرمایه
    /// </summary>
    CapitalDecrease = 2,

    /// <summary>
    /// تغییر ارزش اسمی
    /// </summary>
    NominalValueChange = 3
}
