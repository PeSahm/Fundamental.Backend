using DNTPersianUtils.Core;
using DNTPersianUtils.Core.Normalizer;

namespace Fundamental.Application.Common.Extensions;

public static class StringExtensions
{
    public static string? Safe(this string value)
    {
        value = value.Trim().NormalizeZwnj().ApplyCorrectYeKe();
        return string.IsNullOrWhiteSpace(value) ? null : value;
    }
}