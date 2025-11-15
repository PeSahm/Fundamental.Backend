namespace Fundamental.Domain.Codals.Manufacturing.Enums;

/// <summary>
/// نوع شرکت (اعضای حقوقی هیئت مدیره).
/// </summary>
public enum LegalCompanyType
{
    /// <summary>
    /// Nothing.
    /// </summary>
    Nothing = 0,

    /// <summary>
    /// عمومی.
    /// </summary>
    General = 1,

    /// <summary>
    /// خاص.
    /// </summary>
    Special = 2,

    /// <summary>
    /// محدود.
    /// </summary>
    Limited = 3,

    /// <summary>
    /// ضمانت.
    /// </summary>
    Guaranty = 4,

    /// <summary>
    /// مختلط غیرسهامی.
    /// </summary>
    MotleyNoneShare = 5,

    /// <summary>
    /// مختلط سهامی.
    /// </summary>
    MotleyShare = 6,

    /// <summary>
    /// تعاونی.
    /// </summary>
    Comparative = 7,

    /// <summary>
    /// مشارکت.
    /// </summary>
    Communion = 8,

    /// <summary>
    /// غیر تجاری.
    /// </summary>
    NoneCommerce = 9
}
