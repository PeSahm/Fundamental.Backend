namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// زمانبندی افزایش سرمایه.
/// </summary>
public sealed class ExtraAssemblyScheduling
{
    /// <summary>
    /// به دارندگان حق تقدم سودی تعلق می‌گیرد؟
    /// </summary>
    public bool IsRegistered { get; init; }

    /// <summary>
    /// با توجه به زمان برگزاری مجمع عمومی عادی سالیانه منتهی به.
    /// </summary>
    public string? YearEndToDate { get; init; }
}
