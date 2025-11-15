namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// نوع دستور جلسه مجمع.
/// </summary>
public enum SessionOrderType
{
    /// <summary>
    /// استماع گزارش هیئت مدیره و بازرس قانونی.
    /// </summary>
    ListenedBoardMemberReport = 0,

    /// <summary>
    /// تصویب صورت‌های مالی.
    /// </summary>
    ApproveStatements = 1,

    /// <summary>
    /// انتخاب حسابرس و بازرس قانونی.
    /// </summary>
    SelectInspector = 2,

    /// <summary>
    /// انتخاب روزنامه کثیر‌الانتشار.
    /// </summary>
    SelectNewspaper = 3,

    /// <summary>
    /// انتخاب اعضای هیئت مدیره.
    /// </summary>
    SelectBoardMember = 4,

    /// <summary>
    /// تعیین حق حضور اعضای غیر موظف هیئت مدیره.
    /// </summary>
    BoardMemberWage = 5,

    /// <summary>
    /// تعیین پاداش هیئت مدیره.
    /// </summary>
    BoardMemberGift = 6,

    /// <summary>
    /// سایر موارد.
    /// </summary>
    Other = 8
}
