using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum EnableSubCompany
{
    [Description("غیر فعال")]
    InActive = 0,

    [Description("فعال")]
    Active = 1,

    [Description("فرعی پذیرفته شده")]
    Accepted = 2
}