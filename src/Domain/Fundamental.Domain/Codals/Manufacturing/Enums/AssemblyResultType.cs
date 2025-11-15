namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// نتیجه مجمع عمومی.
/// </summary>
public enum AssemblyResultType
{
    /// <summary>
    /// در خصوص دستور جلسات زیر تصمیم گیری نمود.
    /// </summary>
    DecisionMade = 1,

    /// <summary>
    /// به حد نصاب قانونی نرسید.
    /// </summary>
    QuorumNotReached = 2,

    /// <summary>
    /// در خصوص برخی دستور جلسات تصمیم گیری ننمود و تصمیم گیری به مجمع دیگری موکول گردید.
    /// </summary>
    PartialDecisionDeferred = 3,

    /// <summary>
    /// مجمع در خصوص موارد زیر تصمیم گیری نمود و در مورد بقیه موارد با تنفس روبرو گردید.
    /// </summary>
    PartialDecisionWithBreak = 4,

    /// <summary>
    /// مجمع در خصوص هیچ یک از موارد دستور جلسه تصمیم گیری ننمود و مجمع با تنفس روبرو گردید.
    /// </summary>
    NoDecisionWithBreak = 5
}
