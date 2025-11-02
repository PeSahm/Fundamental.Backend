namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Row code types for currency exchange items.
/// کد ردیف‌های اطلاعاتی مربوط به منابع و مصارف ارزی
/// </summary>
public enum CurrencyExchangeRowCode
{
    /// <summary>
    /// Data row - actual currency exchange transaction entries.
    /// ردیف داده - اطلاعات تراکنش‌های ارزی (فروش صادراتی، وام خارجی، و غیره)
    /// </summary>
    Data = -1,

    /// <summary>
    /// Other sources row - miscellaneous currency sources.
    /// سایر منابع ارزی
    /// </summary>
    OtherSources = 32,

    /// <summary>
    /// Total sources sum - sum of all currency sources.
    /// جمع منابع ارزی
    /// </summary>
    SourcesSum = 33,

    /// <summary>
    /// Other uses row - miscellaneous currency uses.
    /// سایر مصارف ارزی
    /// </summary>
    OtherUses = 36,

    /// <summary>
    /// Total uses sum - sum of all currency uses.
    /// جمع مصارف ارزی
    /// </summary>
    UsesSum = 37
}
