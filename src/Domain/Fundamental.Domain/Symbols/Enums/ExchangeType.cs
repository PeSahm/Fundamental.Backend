using System.ComponentModel;

namespace Fundamental.Domain.Symbols.Enums;

public enum ExchangeType
{
    [Description("بورس")]
    TSE = 1,
    [Description("فرابورس")]
    IFB = 2,
    [Description("بورس انرژی")]
    IRENEX = 3,
    [Description("بورس کالا")]
    IME = 4,
    None = -1
}