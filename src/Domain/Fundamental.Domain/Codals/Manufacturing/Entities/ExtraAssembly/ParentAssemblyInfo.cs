using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;

/// <summary>
/// اطلاعات مجمع اصلی.
/// </summary>
public sealed class ParentAssemblyInfo
{
    /// <summary>
    /// نتیجه مجمع.
    /// </summary>
    public AssemblyResultType AssemblyResultType { get; set; }

    /// <summary>
    /// عنوان نتیجه مجمع.
    /// </summary>
    public string? AssemblyResultTypeTitle { get; set; }

    /// <summary>
    /// تاریخ برگزاری مجمع.
    /// </summary>
    public string? Date { get; set; }

    /// <summary>
    /// ساعت برگزاری مجمع.
    /// </summary>
    public string? Hour { get; set; }

    /// <summary>
    /// محل برگزاری مجمع.
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// روز برگزاری مجمع.
    /// </summary>
    public string? Day { get; set; }

    /// <summary>
    /// تاریخ انتشار اطلاعیه.
    /// </summary>
    public string? LetterPublishDate { get; set; }

    /// <summary>
    /// شماره پیگیری اطلاعیه قبلی.
    /// </summary>
    public int? LetterTracingNo { get; set; }

    /// <summary>
    /// دستور جلسات مجمع.
    /// </summary>
    public List<SessionOrder> SessionOrders { get; set; } = new();
}
