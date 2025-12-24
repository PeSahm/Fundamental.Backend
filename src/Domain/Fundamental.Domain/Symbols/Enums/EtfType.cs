using System.ComponentModel;

namespace Fundamental.Domain.Symbols.Enums;

public enum EtfType
{
    [Description("ای تی اف - ثابت")]
    FixedIncome = 1,

    [Description("ای تی اف - مختلط")]
    MixedIncome = 2,

    [Description("ای تی اف - در سهام")]
    Equity = 3,

    [Description("زمین و ساختمان و پروژه")]
    LandBuildingsAndProjects = 4,

    [Description("طلا")]
    Gold = 5,

    [Description("جسورانه و خصوصی")]
    VcAndPrivateFunds = 6,

    [Description("انرژی")]
    Energy = 7
}