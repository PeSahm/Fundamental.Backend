using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum PublisherSubCompanyType
{
    [Description("عادی")]
    Normal = 0,

    [Description("در حال تصفیه")]
    Liquidation = 1,

    [Description("دارای واحد پولی خارجی")]
    HasForeignCurrencyUnit = 2,

    [Description("دارای واحد پولی خارجی و حسابرس خارجی")]
    HasForeignCurrencyUnitAndForeignAuditor = 3,
}