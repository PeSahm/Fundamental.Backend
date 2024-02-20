using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum PublisherSubCompanyType
{
    [Description("نامشخص")]
    UnKnown = -1,

    [Description("عادی")]
    Normal = 0,

    [Description("در حال تصفیه")]
    Liquidation = 1,

    [Description("دارای واحد پولی خارجی")]
    HasForeignCurrencyUnit = 2,

    [Description("دارای واحد پولی خارجی و حسابرس خارجی")]
    HasForeignCurrencyUnitAndForeignAuditor = 3,

    UnKnown1 = 4,
    UnKnown2 = 5,
    UnKnown3 = 6,
    UnKnown4 = 7,
    UnKnown5 = 9,
    UnKnown6 = 11
}