using Fundamental.Domain.Codals.Manufacturing.Entities.AnnualAssembly;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// تصمیمات مجمع عمومی عادی به‌طور فوق‌العاده (Canonical).
/// Ordinary General Meeting Extraordinary Decisions.
/// </summary>
public class CanonicalExtraAnnualAssembly : BaseEntity<Guid>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CanonicalExtraAnnualAssembly"/> class.
    /// Initializes a new CanonicalExtraAnnualAssembly with core metadata and timestamps.
    /// </summary>
    /// <param name="id">Unique identifier for the aggregate.</param>
    /// <param name="symbol">Market symbol associated with the assembly.</param>
    /// <param name="traceNo">Tracking number of the announcement.</param>
    /// <param name="htmlUrl">URL of the announcement's HTML page.</param>
    /// <param name="fiscalYear">Fiscal year applicable to the assembly.</param>
    /// <param name="yearEndMonth">Fiscal year end month.</param>
    /// <param name="reportMonth">Month the report/assembly relates to.</param>
    /// <param name="assemblyDate">Date when the assembly was held.</param>
    /// <param name="publishDate">Date the announcement was published.</param>
    /// <param name="version">JSON version identifier for the record.</param>
    public CanonicalExtraAnnualAssembly(
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

    /// <summary>
    /// Initializes a new instance of the <see cref="CanonicalExtraAnnualAssembly"/> class.
    /// Initializes a new instance of CanonicalExtraAnnualAssembly for ORM and deserialization purposes.
    /// </summary>
    protected CanonicalExtraAnnualAssembly()
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
    public Symbol Symbol { get; private set; }

    /// <summary>
    /// شماره پیگیری.
    /// </summary>
    public ulong TraceNo { get; private set; }

    /// <summary>
    /// URL صفحه HTML اطلاعیه.
    /// </summary>
    public Uri HtmlUrl { get; private set; }

    /// <summary>
    /// سال مالی (Persian year).
    /// Extracted from parentAssembly.date.
    /// </summary>
    public FiscalYear FiscalYear { get; private set; }

    /// <summary>
    /// واحد پولی (همیشه IRR).
    /// </summary>
    public IsoCurrency Currency { get; private set; }

    /// <summary>
    /// ماه پایان سال مالی.
    /// Usually 12 for standard Persian calendar fiscal years.
    /// Extracted from parentAssembly.date.
    /// </summary>
    public StatementMonth YearEndMonth { get; private set; }

    /// <summary>
    /// ماه گزارش (برابر با ماه برگزاری مجمع).
    /// Report month from assembly date.
    /// </summary>
    public StatementMonth ReportMonth { get; private set; }

    /// <summary>
    /// تاریخ برگزاری مجمع.
    /// </summary>
    public DateTime AssemblyDate { get; private set; }

    // === Owned Entities (preserve JSON structure) ===

    /// <summary>
    /// اطلاعات اصلی مجمع.
    /// Maps to parentAssembly in JSON.
    /// </summary>
    public ParentAssembly? ParentAssemblyInfo { get; set; }

    /// <summary>
    /// اعضای اصلی مجمع (رئیس، ناظرین، منشی).
    /// Maps to assemblyChiefMembers in JSON.
    /// </summary>
    public AssemblyChiefMembers? AssemblyChiefMembersInfo { get; set; }

    // === Additional Meeting Info (at root level in JSON) ===

    /// <summary>
    /// مدت انتخاب اعضای هیئت مدیره (سال).
    /// </summary>
    public int? BoardMemberPeriod { get; set; }

    /// <summary>
    /// توضیحات انتشار اوراق بهادار.
    /// </summary>
    public string? PublishSecurityDescription { get; set; }

    /// <summary>
    /// سایر توضیحات.
    /// </summary>
    public string? OtherDescription { get; set; }

    /// <summary>
    /// ساعت برگزاری مجمع بعدی (در صورت تنفس).
    /// </summary>
    public string? NewHour { get; set; }

    /// <summary>
    /// روز برگزاری مجمع بعدی (در صورت تنفس).
    /// </summary>
    public string? NewDay { get; set; }

    /// <summary>
    /// تاریخ برگزاری مجمع بعدی (در صورت تنفس).
    /// </summary>
    public string? NewDate { get; set; }

    /// <summary>
    /// محل برگزاری مجمع بعدی (در صورت تنفس).
    /// </summary>
    public string? NewLocation { get; set; }

    /// <summary>
    /// توضیحات اعلام تنفس.
    /// </summary>
    public string? BreakDescription { get; set; }

    // === Collections (stored as JSONB) ===

    /// <summary>
    /// اطلاعات سهامداران.
    /// </summary>
    public ICollection<ShareHolder> ShareHolders { get; set; } = new List<ShareHolder>();

    /// <summary>
    /// اعضای هیئت مدیره حاضر در مجمع.
    /// </summary>
    public ICollection<AssemblyBoardMember> AssemblyBoardMembers { get; set; } = new List<AssemblyBoardMember>();

    /// <summary>
    /// حسابرسان و بازرسان.
    /// </summary>
    public ICollection<Inspector> Inspectors { get; set; } = new List<Inspector>();

    /// <summary>
    /// اعضای جدید هیئت مدیره.
    /// </summary>
    public ICollection<NewBoardMember> NewBoardMembers { get; set; } = new List<NewBoardMember>();

    /// <summary>
    /// حق حضور و پاداش هیئت مدیره.
    /// </summary>
    public ICollection<BoardMemberWageAndGift> BoardMemberWageAndGifts { get; set; } = new List<BoardMemberWageAndGift>();

    /// <summary>
    /// روزنامه‌های کثیرالانتشار.
    /// </summary>
    public ICollection<NewsPaper> NewsPapers { get; set; } = new List<NewsPaper>();

    /// <summary>
    /// اطلاعات میان دوره‌ای.
    /// </summary>
    public ICollection<AssemblyInterim> AssemblyInterims { get; set; } = new List<AssemblyInterim>();

    /// <summary>
    /// سود انباشته تخصیص یافته.
    /// </summary>
    public ICollection<ProportionedRetainedEarning> ProportionedRetainedEarnings { get; set; } = new List<ProportionedRetainedEarning>();

    // === Owned Nullable Entities ===

    /// <summary>
    /// مدیرعامل.
    /// </summary>
    public AssemblyAttendee? Ceo { get; set; }

    /// <summary>
    /// رئیس کمیته حسابرسی.
    /// </summary>
    public AssemblyAttendee? AuditCommitteeChairman { get; set; }

    /// <summary>
    /// نماینده حسابرس و بازرس قانونی.
    /// </summary>
    public AssemblyAttendee? IndependentAuditorRepresentative { get; set; }

    /// <summary>
    /// بالاترین مقام مالی.
    /// </summary>
    public AssemblyAttendee? TopFinancialPosition { get; set; }
}