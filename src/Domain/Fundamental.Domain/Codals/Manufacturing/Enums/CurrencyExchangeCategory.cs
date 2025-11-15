namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// Category types for currency exchange items.
/// دسته‌بندی اقلام منابع و مصارف ارزی.
/// </summary>
public enum CurrencyExchangeCategory
{
    /// <summary>
    /// Total/Sum rows - aggregate totals for sources or uses.
    /// جمع کل - ردیف‌های جمع‌بندی.
    /// </summary>
    Total = 0,

    /// <summary>
    /// Currency sources - foreign currency inflows (exports, loans, etc.).
    /// منابع ارزی - ورودی‌های ارزی (فروش صادراتی، وام خارجی و غیره).
    /// </summary>
    Sources = 1,

    /// <summary>
    /// Currency uses - foreign currency outflows (imports, payments, etc.).
    /// مصارف ارزی - خروجی‌های ارزی (خرید وارداتی، پرداخت‌ها و غیره).
    /// </summary>
    Uses = 2
}