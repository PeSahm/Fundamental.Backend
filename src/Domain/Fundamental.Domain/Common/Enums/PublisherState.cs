using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum PublisherState
{
    [Description("ثبت شده در بورس کالا")]
    RegisterInIme = 4,

    [Description("ثبت شده در بورس انرژی")]
    RegisterInIrenex = 5,

    [Description("ثبت شده در بورس تهران")]
    RegisterInTse = 0,

    [Description("ثبت شده در فرابورس")]
    RegisterInIfb = 1,

    [Description("ثبت شده پذیرفته نشده")]
    RegisteredNotAccepted = 2,

    [Description("ثبت نشده در سازمان")]
    NotRegistered = 3
}