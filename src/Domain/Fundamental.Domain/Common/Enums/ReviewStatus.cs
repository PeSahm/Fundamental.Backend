using System.ComponentModel;

namespace Fundamental.Domain.Common.Enums;

public enum ReviewStatus
{
    [Description("در انتظار تایید")]
    Pending = 1,

    [Description("رد شده")]
    Rejected = 2,

    [Description("تایید شده")]
    Approved = 3,
}