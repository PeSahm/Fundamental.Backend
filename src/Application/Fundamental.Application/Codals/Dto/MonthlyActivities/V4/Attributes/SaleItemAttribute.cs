using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;

namespace Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class SaleItemAttribute : Attribute
{
    public SaleItemAttribute(SaleColumnId saleColumnId)
    {
        SaleColumnId = saleColumnId;
    }

    public SaleColumnId SaleColumnId { get; }
}