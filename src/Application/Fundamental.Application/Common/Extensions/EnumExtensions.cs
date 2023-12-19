using System.ComponentModel;
using System.Reflection;
using DNTPersianUtils.Core;

namespace Fundamental.Application.Common.Extensions;

public static class EnumExtensions
{
    public static string? GetDescription<T>(this T enumValue)
        where T : struct, IConvertible
    {
        if (!typeof(T).IsEnum)
        {
            return null;
        }

        string? description = enumValue.ToString();
        FieldInfo? fieldInfo = enumValue.GetType().GetField(enumValue.ToString()!);

        if (fieldInfo != null)
        {
            object[] attrs = fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), true);

            if (attrs.Length > 0)
            {
                description = ((DescriptionAttribute)attrs[0]).Description;
            }
        }

        return description?.NormalizePersianText(PersianNormalizers.CleanupZwnj |
                                                 PersianNormalizers.RemoveDiacritics |
                                                 PersianNormalizers.RemoveAllKashida |
                                                 PersianNormalizers.RemoveHexadecimalSymbols |
                                                 PersianNormalizers.ApplyPersianYeKe |
                                                 PersianNormalizers.ApplyHalfSpaceRule |
                                                 PersianNormalizers.RemoveOutsideInsideSpacing
        );
    }
}