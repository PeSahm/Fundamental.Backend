namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// اطلاعات میان دوره‌ای.
/// </summary>
public class AssemblyInterim
{
    /// <summary>
    /// نام فیلد.
    /// </summary>
    public string? FieldName { get; set; }

    /// <summary>
    /// توضیحات.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// مقدار سال منتهی به.
    /// </summary>
    public decimal? YearEndToDateValue { get; set; }

    /// <summary>
    /// درصد تغییرات.
    /// </summary>
    public decimal? Percent { get; set; }

    /// <summary>
    /// دلایل تغییرات.
    /// </summary>
    public string? ChangesReason { get; set; }

    /// <summary>
    /// کلاس ردیف.
    /// </summary>
    public string? RowClass { get; set; }
}
