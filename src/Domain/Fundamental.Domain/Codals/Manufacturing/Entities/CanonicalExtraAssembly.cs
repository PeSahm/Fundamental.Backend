using Fundamental.Domain.Codals.Manufacturing.Entities.ExtraAssembly;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// تصمیمات مجمع عمومی فوق‌العاده (Canonical).
/// </summary>
public class CanonicalExtraAssembly : BaseEntity<Guid>
{
    public CanonicalExtraAssembly(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        Uri htmlUrl,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        DateTime assemblyDate,
        DateTime publishDate,
        string version
    )
    {
        Id = id;
        Symbol = symbol;
        TraceNo = traceNo;
        HtmlUrl = htmlUrl;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        AssemblyDate = assemblyDate;
        PublishDate = publishDate;
        Currency = IsoCurrency.IRR;
        Version = version;
        CreatedAt = DateTime.UtcNow;
    }

    protected CanonicalExtraAssembly()
    {
    }

    /// <summary>
    /// نسخه JSON (V1).
    /// </summary>
    public string Version { get; private set; }

    /// <summary>
    /// تاریخ انتشار اطلاعیه (Gregorian).
    /// </summary>
    public DateTime PublishDate { get; private set; }

    /// <summary>
    /// Symbol navigation property.
    /// </summary>
    public Symbol Symbol { get; private set; } = null!;

    /// <summary>
    /// شماره پیگیری.
    /// </summary>
    public ulong TraceNo { get; private set; }

    /// <summary>
    /// URL صفحه HTML اطلاعیه.
    /// </summary>
    public Uri HtmlUrl { get; private set; } = null!;

    /// <summary>
    /// سال مالی (Persian year).
    /// Extracted from parentAssembly.date.
    /// </summary>
    public FiscalYear FiscalYear { get; private set; } = null!;

    /// <summary>
    /// واحد پولی (همیشه IRR).
    /// </summary>
    public IsoCurrency Currency { get; private set; }

    /// <summary>
    /// ماه پایان سال مالی.
    /// Usually 12 for standard Persian calendar fiscal years.
    /// Extracted from parentAssembly.date or defaults to 12.
    /// </summary>
    public StatementMonth YearEndMonth { get; private set; } = null!;

    /// <summary>
    /// ماه گزارش (برابر با ماه برگزاری مجمع).
    /// Report month from assembly date.
    /// </summary>
    public StatementMonth ReportMonth { get; private set; } = null!;

    /// <summary>
    /// تاریخ برگزاری مجمع.
    /// </summary>
    public DateTime AssemblyDate { get; private set; }

    // === Owned Entities (preserve JSON nested structure) ===

    /// <summary>
    /// اطلاعات مجمع اصلی (شامل نتیجه، تاریخ، ساعت، محل، دستور جلسات).
    /// </summary>
    public ParentAssemblyInfo? ParentAssemblyInfo { get; set; }

    /// <summary>
    /// اعضای هیئت رئیسه مجمع.
    /// </summary>
    public AssemblyChiefMembersInfo? AssemblyChiefMembersInfo { get; set; }

    /// <summary>
    /// اطلاعات مجمع بعدی - اعلام تنفس.
    /// </summary>
    public NextSessionInfo? NextSessionInfo { get; set; }

    /// <summary>
    /// زمانبندی افزایش سرمایه.
    /// </summary>
    public ExtraAssemblyScheduling? ExtraAssemblyScheduling { get; set; }

    /// <summary>
    /// اطلاعات کاهش سرمایه.
    /// </summary>
    public ExtraAssemblyDecreaseCapital? ExtraAssemblyDecreaseCapital { get; set; }

    /// <summary>
    /// اطلاعات تغییر ارزش اسمی.
    /// </summary>
    public ExtraAssemblyShareValueChangeCapital? ExtraAssemblyShareValueChangeCapital { get; set; }

    // === Root-level Collections ===

    /// <summary>
    /// اطلاعات ترکیب سهامداران.
    /// </summary>
    public ICollection<ShareHolder> ShareHolders { get; set; } = new List<ShareHolder>();

    /// <summary>
    /// اعضای هیئت مدیره حاضر.
    /// </summary>
    public ICollection<AssemblyBoardMember> AssemblyBoardMembers { get; set; } = new List<AssemblyBoardMember>();

    /// <summary>
    /// افزایش سرمایه.
    /// </summary>
    public ICollection<ExtraAssemblyIncreaseCapital> ExtraAssemblyIncreaseCapitals { get; set; } = new List<ExtraAssemblyIncreaseCapital>();

    // === Root-level Properties (not nested in JSON) ===

    /// <summary>
    /// وضعیت تغییر سرمایه.
    /// </summary>
    public CapitalChangeState CapitalChangeState { get; set; }

    /// <summary>
    /// آخرین سرمایه ثبت شده - ارزش اسمی هر سهم (ریال).
    /// </summary>
    public int? LastShareValue { get; set; }

    /// <summary>
    /// آخرین سرمایه ثبت شده - مبلغ (میلیون ریال).
    /// </summary>
    public int? LastCapital { get; set; }

    /// <summary>
    /// آخرین سرمایه ثبت شده - تعداد سهام.
    /// </summary>
    public long? LastShareCount { get; set; }

    /// <summary>
    /// آدرس قبلی.
    /// </summary>
    public string? OldAddress { get; set; }

    /// <summary>
    /// آدرس جدید.
    /// </summary>
    public string? NewAddress { get; set; }

    /// <summary>
    /// نام قبلی.
    /// </summary>
    public string? OldName { get; set; }

    /// <summary>
    /// نام جدید.
    /// </summary>
    public string? NewName { get; set; }

    /// <summary>
    /// عنوان فعالیت قبلی.
    /// </summary>
    public string? OldActivitySubject { get; set; }

    /// <summary>
    /// عنوان فعالیت جدید.
    /// </summary>
    public string? NewActivitySubject { get; set; }

    /// <summary>
    /// سال مالی قبلی - دوره (ماه).
    /// </summary>
    public int? OldFinancialYearMonthLength { get; set; }

    /// <summary>
    /// سال مالی قبلی.
    /// </summary>
    public string? OldFinancialYearEndDate { get; set; }

    /// <summary>
    /// سال مالی قبلی - دوره (روز).
    /// </summary>
    public int? OldFinancialYearDayLength { get; set; }

    /// <summary>
    /// سال مالی جدید.
    /// </summary>
    public string? NewFinancialYearEndDate { get; set; }

    /// <summary>
    /// سال مالی جدید - دوره (ماه).
    /// </summary>
    public string? NewFinancialYearMonthLength { get; set; }

    /// <summary>
    /// سال مالی جدید - دوره (روز).
    /// </summary>
    public string? NewFinancialYearDayLength { get; set; }

    /// <summary>
    /// تغییر محل.
    /// </summary>
    public bool IsLocationChange { get; set; }

    /// <summary>
    /// تغییر نام.
    /// </summary>
    public bool IsNameChange { get; set; }

    /// <summary>
    /// تغییر نوع فعالیت.
    /// </summary>
    public bool IsActivitySubjectChange { get; set; }

    /// <summary>
    /// تغییر سال مالی منتهی به.
    /// </summary>
    public bool IsFinancialYearChange { get; set; }

    /// <summary>
    /// تصمیم گیری در خصوص شمولیت مفاد ماده 141 قانون تجارت.
    /// </summary>
    public bool IsDecidedClause141 { get; set; }

    /// <summary>
    /// تصمیم گیری در خصوص شمولیت مفاد ماده 141 قانون تجارت - توضیحات.
    /// </summary>
    public string? DecidedClause141Des { get; set; }

    /// <summary>
    /// تطابق اساسنامه شرکت با نمونه اساسنامه سازمان بورس.
    /// </summary>
    public bool IsAccordWithSEOStatuteApproved { get; set; }

    /// <summary>
    /// سایر موارد - توضیحات.
    /// </summary>
    public string? OtherDes { get; set; }

    /// <summary>
    /// شماره پیگیری مجوز افزایش سرمایه.
    /// </summary>
    public int? PrimaryMarketTracingNo { get; set; }

    /// <summary>
    /// اصلاح اساسنامه.
    /// </summary>
    public bool CorrectionStatuteApproved { get; set; }

    // === Individual Attendees (nullable owned entities) ===

    /// <summary>
    /// مدیرعامل.
    /// </summary>
    public AssemblyAttendee? Ceo { get; set; }

    /// <summary>
    /// رئیس کمیته حسابرسی.
    /// </summary>
    public AssemblyAttendee? AuditCommitteeChairman { get; set; }
}
