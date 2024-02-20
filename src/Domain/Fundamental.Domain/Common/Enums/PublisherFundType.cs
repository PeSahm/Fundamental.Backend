using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum PublisherFundType
{
    [Description("نامشخص")]
    UnKnown = -1,

    [Description("صندوق نیست")]
    NotAFund = 0,

    [Description("صندوق سرمایه گذاری زمین و ساختمان")]
    RealEstate = 1,

    [Description("صندوق سرمایه گذاری درآمد ثابت")]
    FixedIncome = 2,

    [Description("صندوق سرمایه گذاری مختلط")]
    Mixed = 3,

    [Description("صندوق سرمایه گذاری سهام")]
    Equity = 4,

    [Description("صندوق سرمایه گذاری پروژه")]
    Project = 5,

    [Description("صندوق سرمایه گذاری جسورانه")]
    Venture = 6,

    [Description("صندوق سرمایه گذاری بازارگردانی")]
    MarketMaking = 7,

    [Description("صندوق سرمایه گذاری کالایی")]
    Commodity = 8,

    [Description("صندوق سرمایه گذاری غیرمتنوع")]
    Diversified = 9,
}