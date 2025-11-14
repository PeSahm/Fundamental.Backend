using System.Text.Json;
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

    public static bool IsValidJson(this string str)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            return false;
        }

        str = str.Trim();

        if ((str.StartsWith('{') && str.EndsWith('}')) ||
            (str.StartsWith('[') && str.EndsWith(']')))
        {
            try
            {
                JsonDocument.Parse(str);
                return true;
            }
            catch (JsonException)
            {
                // Exception means it's not a valid JSON
                return false;
            }
        }

        return false;
    }
}