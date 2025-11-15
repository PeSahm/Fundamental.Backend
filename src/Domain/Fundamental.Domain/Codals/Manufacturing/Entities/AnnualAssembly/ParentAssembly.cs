using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;

/// <summary>
/// اطلاعات اصلی مجمع (Parent Assembly).
/// </summary>
public class ParentAssembly
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
    /// شماره پیگیری اطلاعیه قبلی.
    /// </summary>
    public ulong? LetterTracingNo { get; set; }

    /// <summary>
    /// دستور جلسات مجمع.
    /// </summary>
    public List<SessionOrder> SessionOrders { get; set; } = new();
}
