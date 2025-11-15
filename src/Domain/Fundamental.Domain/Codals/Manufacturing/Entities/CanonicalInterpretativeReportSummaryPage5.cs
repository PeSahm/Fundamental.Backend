using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Domain.Codals.Manufacturing.Entities;

/// <summary>
/// Canonical model for Interpretative Report Summary Page 5 data, normalized from V2 JSON.
/// This entity represents page 5 of the interpretative report summary (گزیده گزارش تفسیری مدیریت - صفحه 5),
/// containing other operating income, non-operating expenses, financing details, and investment income.
/// </summary>
public class CanonicalInterpretativeReportSummaryPage5 : BaseEntity<Guid>
{
    public CanonicalInterpretativeReportSummaryPage5(
        Guid id,
        Symbol symbol,
        ulong traceNo,
        string uri,
        FiscalYear fiscalYear,
        StatementMonth yearEndMonth,
        StatementMonth reportMonth,
        DateTime publishDate,
        string version
    )
    {
        Id = id;
        Symbol = symbol;
        TraceNo = traceNo;
        Uri = uri;
        FiscalYear = fiscalYear;
        YearEndMonth = yearEndMonth;
        ReportMonth = reportMonth;
        PublishDate = publishDate;
        Version = version;
        OtherOperatingIncomes = new List<OtherOperatingIncomeItem>();
        OtherNonOperatingExpenses = new List<OtherNonOperatingExpenseItem>();
        FinancingDetails = new List<FinancingDetailItem>();
        FinancingDetailsEstimated = new List<FinancingDetailItem>();
        InvestmentIncomes = new List<InvestmentIncomeItem>();
        MiscellaneousExpenses = new List<MiscellaneousExpenseItem>();
        Descriptions = new List<InterpretativeReportDescription>();
        CreatedAt = DateTime.UtcNow;
    }

    protected CanonicalInterpretativeReportSummaryPage5()
    {
    }

    /// <summary>
    /// Version of the Interpretative Report Summary Page 5 data (e.g., "V2").
    /// Indicates which CODAL format version this data was extracted from.
    /// </summary>
    public string Version { get; private set; }

    /// <summary>
    /// Publish date of the interpretative report (Gregorian).
    /// Sourced from CODAL statement's PublishDateMiladi.
    /// </summary>
    public DateTime? PublishDate { get; private set; }

    /// <summary>
    /// Navigation property to the associated symbol.
    /// The company/entity this interpretative report belongs to.
    /// </summary>
    public Symbol Symbol { get; private set; } = null!;

    /// <summary>
    /// Trace number from the CODAL API.
    /// Unique identifier for this specific report instance.
    /// </summary>
    public ulong TraceNo { get; private set; }

    /// <summary>
    /// URI of the original report.
    /// Link to the source CODAL document for reference.
    /// </summary>
    public string Uri { get; private set; } = null!;

    /// <summary>
    /// Fiscal year of the report.
    /// The Persian calendar year this report covers (e.g., 1404).
    /// Extracted from yearEndToDate in yearData array.
    /// </summary>
    public FiscalYear FiscalYear { get; private set; } = null!;

    /// <summary>
    /// Currency used in the report.
    /// Typically IRR (Iranian Rial) for domestic reports.
    /// </summary>
    public IsoCurrency Currency { get; private set; } = IsoCurrency.IRR;

    /// <summary>
    /// Year-end month of the fiscal year.
    /// Usually 12 for standard Persian calendar fiscal years.
    /// Extracted from yearEndToDate in yearData array.
    /// </summary>
    public StatementMonth YearEndMonth { get; private set; } = null!;

    /// <summary>
    /// Report month/period end.
    /// The month this report period ends (1-12).
    /// Extracted from periodEndToDate in yearData array.
    /// </summary>
    public StatementMonth ReportMonth { get; private set; } = null!;

    /// <summary>
    /// Collection of other operating income items (سایر درآمدهای عملیاتی).
    /// Maps to interpretativeReportSummaryPage5.otherOperatingIncome.rowItems.
    /// Includes items like foreign exchange gains, service income, and other operating revenues.
    /// Use static helpers on OtherOperatingIncomeItem to filter data rows and get totals.
    /// </summary>
    public ICollection<OtherOperatingIncomeItem> OtherOperatingIncomes { get; set; } = new List<OtherOperatingIncomeItem>();

    /// <summary>
    /// Collection of other non-operating expense items (سایر هزینه‌های غیرعملیاتی).
    /// Maps to interpretativeReportSummaryPage5.otherNonOperatingExpenses.rowItems.
    /// Includes miscellaneous non-operating expenses and foreign exchange losses.
    /// Use static helpers on OtherNonOperatingExpenseItem to filter data rows and get totals.
    /// </summary>
    public ICollection<OtherNonOperatingExpenseItem> OtherNonOperatingExpenses { get; set; } = new List<OtherNonOperatingExpenseItem>();

    /// <summary>
    /// Collection of financing detail items for current period (جزئیات تامین مالی شرکت در پایان دوره).
    /// Maps to interpretativeReportSummaryPage5.detailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod.rowItems.
    /// Includes bank facilities, loans, interest rates, and financial expenses.
    /// Use static helpers on FinancingDetailItem to get specific rows (bank facilities, settled, total, etc.).
    /// </summary>
    public ICollection<FinancingDetailItem> FinancingDetails { get; set; } = new List<FinancingDetailItem>();

    /// <summary>
    /// Collection of estimated financing detail items for future period (برآورد تامین مالی).
    /// Maps to interpretativeReportSummaryPage5.detailsOfTheFinancingOfTheCompany-Est1.rowItems.
    /// Contains estimated/projected financing information.
    /// Use static helpers on FinancingDetailItem to get specific rows.
    /// </summary>
    public ICollection<FinancingDetailItem> FinancingDetailsEstimated { get; set; } = new List<FinancingDetailItem>();

    /// <summary>
    /// Collection of investment income items (درآمدهای سرمایه‌گذاری).
    /// Maps to interpretativeReportSummaryPage5.nonOperationIncomeAndExpensesInvestmentIncome.rowItems.
    /// Includes bank deposit interest, dividend income from investments, foreign exchange gains on non-operating assets.
    /// Use static helpers on InvestmentIncomeItem to filter data rows and get totals.
    /// </summary>
    public ICollection<InvestmentIncomeItem> InvestmentIncomes { get; set; } = new List<InvestmentIncomeItem>();

    /// <summary>
    /// Collection of miscellaneous expense items (اقلام متفرقه).
    /// Maps to interpretativeReportSummaryPage5.nonOperationIncomeAndExpensesMiscellaneousItems.rowItems.
    /// Includes prior year taxes, other miscellaneous items, and adjustments.
    /// Use static helpers on MiscellaneousExpenseItem to filter data rows and get totals.
    /// </summary>
    public ICollection<MiscellaneousExpenseItem> MiscellaneousExpenses { get; set; } = new List<MiscellaneousExpenseItem>();

    /// <summary>
    /// Collection of descriptive text items and notes.
    /// Maps to various description sections: p5Desc1, p5Desc2, descriptionForDetailsOfTheFinancingOfTheCompanyAtTheEndOfThePeriod,
    /// companyEstimatesOfFinancingProgramsAndCompanyFinanceChanges, corporateIncomeProgram, otherImportantPrograms, otherImportantNotes.
    /// Contains free-form text descriptions, disclaimers, and additional notes.
    /// Check SectionName property to identify which section each description belongs to.
    /// </summary>
    public ICollection<InterpretativeReportDescription> Descriptions { get; set; } = new List<InterpretativeReportDescription>();
}
