using System.ComponentModel;

namespace Fundamental.Domain.Symbols.Enums;

public enum ProductType
{
    [Description("سهم")]
    Equity = 1,

    [Description("صندوق")]
    Fund = 2,

    [Description("اوراق")]
    Bond = 3,

    [Description("اختیار فروش")]
    OptionSell = 4,

    [Description("شاخص")]
    Index = 5,

    [Description("سلف")]
    Forward = 6,

    [Description("ای تی اف")]
    ETF = 7,

    [Description("VC")]
    VC = 8,

    [Description("آتی")]
    Futures = 9,

    [Description("گواهی سپرده")]
    CertificateOfDeposit = 10,

    [Description("اوراق مشارکت بانکی")]
    Coupon = 11,

    [Description("تسهیلات مسکن")]
    MBS = 12,

    [Description("سکه - تحویل یک روزه")]
    GoldCoin = 13,

    [Description("اختیار خرید")]
    OptionBuy = 14,

    [Description("برق انرژی")]
    EnergyElectricity = 15,

    [Description("دارایی فکری")]
    IntellectualProperty = 16,

    [Description("گواهی سپرده کالایی")]
    IMECertificate = 17,

    [Description(" گواهی سپرده کالایی کشاورزی")]
    IMECertificateAgriculture = 18,

    [Description("گواهی سپرده کالایی شیشه")]
    IMECertificateGlass = 19,

    [Description("سایر")]
    Other = 20,
    [Description("گواهی صرفه جویی برق")]
    EnergySavingCertificate = 21,

    [Description("همه")]
    All = -1
}