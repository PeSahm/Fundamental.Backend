using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum PublisherMarketType
{
    [Description("هیچکدام")]
    None = -1,

    [Description("بازار اول")]
    First = 0,

    [Description("بازار دوم")]
    Second = 1,

    [Description("بازار پایه")]
    Base = 2,

    [Description("شرکتهای کوچک و متوسط")]
    SmallAndMedium = 3,
}