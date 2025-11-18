using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// سود انباشته تخصیص یافته.
/// </summary>
public class ProportionedRetainedEarning
{
    /// <summary>
    /// نام فیلد.
    /// </summary>
    public ProportionedRetainedEarningFieldName? FieldName { get; set; }

    /// <summary>
    /// توضیحات.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// مقدار سال منتهی به.
    /// </summary>
    public decimal? YearEndToDateValue { get; set; }

    /// <summary>
    /// کلاس ردیف.
    /// </summary>
    public string? RowClass { get; set; }

    /// <summary>
    /// Gets a specific field value from a collection by field name.
    /// </summary>
    /// <param name="items">Collection of proportioned retained earnings.</param>
    /// <param name="fieldName">The field name to search for.</param>
    /// <returns>The item matching the field name, or null if not found.</returns>
    public static ProportionedRetainedEarning? GetByFieldName(
        IEnumerable<ProportionedRetainedEarning> items,
        ProportionedRetainedEarningFieldName fieldName)
    {
        if (items == null)
        {
            return null;
        }

        return items.FirstOrDefault(x => x.FieldName == fieldName);
    }

    /// <summary>
    /// Gets the value for a specific field from a collection.
    /// </summary>
    /// <param name="items">Collection of proportioned retained earnings.</param>
    /// <param name="fieldName">The field name to search for.</param>
    /// <returns>The value if found, null otherwise.</returns>
    public static decimal? GetValue(
        IEnumerable<ProportionedRetainedEarning> items,
        ProportionedRetainedEarningFieldName fieldName)
    {
        return GetByFieldName(items, fieldName)?.YearEndToDateValue;
    }
}
