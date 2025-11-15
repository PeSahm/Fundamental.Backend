using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// حق حضور و پاداش هیئت مدیره.
/// </summary>
public class BoardMemberWageAndGift
{
    /// <summary>
    /// نوع فیلد.
    /// </summary>
    public WageAndGiftFieldType Type { get; set; }

    /// <summary>
    /// عنوان فیلد.
    /// </summary>
    public string? FieldName { get; set; }

    /// <summary>
    /// سال جاری - مبلغ.
    /// </summary>
    public decimal? CurrentYearValue { get; set; }

    /// <summary>
    /// سال قبل - مبلغ.
    /// </summary>
    public decimal? PastYearValue { get; set; }

    /// <summary>
    /// توضیحات.
    /// </summary>
    public string? Description { get; set; }
}
