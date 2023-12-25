using System.ComponentModel;
using System.Reflection;
using DNTPersianUtils.Core;
using Fundamental.Domain.Attributes;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Exceptions;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

public class BalanceSheetSort : BaseEntity<Guid>
{
    public BalanceSheetSort(
        Guid id,
        ushort order,
        ushort codalRow,
        BalanceSheetCategory category,
        DateTime createdAt
    )
    {
        Id = id;
        Order = order;
        Category = category;
        SetCodaRow(codalRow);
        Description = GetDescription(codalRow, category);
        CreatedAt = createdAt;
    }

    public ushort Order { get; private set; }

    public ushort CodalRow { get; private set; }

    public string Description { get; private set; }

    public BalanceSheetCategory Category { get; private set; }

    public static bool CheckCodalRow(ushort codalRow)
    {
        foreach (PropertyInfo property in typeof(BalanceSheetRow).GetProperties())
        {
            if ((ushort)(property.GetValue(null) ?? 0) == codalRow)
            {
                return true;
            }
        }

        return false;
    }

    public static string GetDescription(ushort value, BalanceSheetCategory category)
    {
        foreach (PropertyInfo property in typeof(BalanceSheetRow).GetProperties())
        {
            if ((ushort)(property.GetValue(null) ?? 0) == value)
            {
                BalanceSheetCategoryAttribute? categoryAttribute = property.GetCustomAttribute<BalanceSheetCategoryAttribute>();

                if (categoryAttribute is null)
                {
                    continue;
                }

                if (categoryAttribute.BalanceSheetCategory != category)
                {
                    continue;
                }

                DescriptionAttribute? attr = property.GetCustomAttribute<DescriptionAttribute>();

                return attr?.Description.ApplyCorrectYeKe() ?? string.Empty;
            }
        }

        return "Invalid value";
    }

    public void SetCodaRow(ushort codalRow)
    {
        if (!CheckCodalRow(codalRow))
        {
            throw new InvalidBalanceSheetRowCodeException(codalRow);
        }

        CodalRow = codalRow;
    }
}