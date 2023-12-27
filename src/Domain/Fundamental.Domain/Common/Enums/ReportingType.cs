using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

/// <summary>
/// انواع صنعت.
/// </summary>
public enum ReportingType
{
    [Description("تولیدی")]
    Production = 1000000,

    [Description("ساختمانی")]
    Structural = 1000001,

    [Description("سرمایه گذاری")]
    Investment = 1000002,

    [Description("بانک")]
    Bank = 1000003,

    [Description("لیزینگ")]
    Leasing = 1000004,

    [Description("خدماتی")]
    Services = 1000005,

    [Description("بیمه")]
    Insurance = 1000006,

    [Description("حمل و نقل دریایی")]
    MaritimeTransportation = 1000007,

    [Description("نامشخص")]
    UnKnown = -1
}