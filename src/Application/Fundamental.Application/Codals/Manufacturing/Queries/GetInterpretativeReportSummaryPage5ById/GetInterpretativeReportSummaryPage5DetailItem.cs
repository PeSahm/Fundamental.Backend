using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;

public sealed record GetInterpretativeReportSummaryPage5DetailItem(
    Guid Id,
    string Isin,
    string Symbol,
    string Title,
    string Uri,
    string Version,
    int FiscalYear,
    int YearEndMonth,
    int ReportMonth,
    ulong TraceNo,
    DateTime? PublishDate,
    List<OtherOperatingIncomeItemDto> OtherOperatingIncomes,
    List<OtherNonOperatingExpenseItemDto> OtherNonOperatingExpenses,
    List<FinancingDetailItemDto> FinancingDetails,
    List<FinancingDetailItemDto> FinancingDetailsEstimated,
    List<InvestmentIncomeItemDto> InvestmentIncomes,
    List<MiscellaneousExpenseItemDto> MiscellaneousExpenses,
    List<InterpretativeReportDescriptionDto> Descriptions
);

public sealed record OtherOperatingIncomeItemDto(
    OtherOperatingIncomeRowCode RowCode,
    int Category,
    InterpretativeReportRowType? RowType,
    string? Id,
    string? ItemDescription,
    decimal? PreviousPeriodAmount,
    decimal? CurrentPeriodAmount,
    decimal? EstimatedPeriodAmount
);

public sealed record OtherNonOperatingExpenseItemDto(
    OtherNonOperatingExpenseRowCode RowCode,
    int Category,
    InterpretativeReportRowType? RowType,
    string? Id,
    string? ItemDescription,
    decimal? PreviousPeriodAmount,
    decimal? CurrentPeriodAmount,
    decimal? EstimatedPeriodAmount
);

public sealed record FinancingDetailItemDto(
    FinancingDetailRowCode RowCode,
    int Category,
    InterpretativeReportRowType? RowType,
    string? FinancingSource,
    decimal? InterestRate,
    decimal? BeginningBalance,
    decimal? EndingBalanceRial,
    string? CurrencyType,
    decimal? ForeignAmount,
    decimal? ForeignRialEquivalent,
    decimal? ShortTerm,
    decimal? LongTerm,
    decimal? FinancialExpense,
    string? OtherDescriptions
);

public sealed record InvestmentIncomeItemDto(
    InvestmentIncomeRowCode RowCode,
    int Category,
    InterpretativeReportRowType? RowType,
    string? Id,
    string? ItemDescription,
    decimal? PreviousPeriodAmount,
    decimal? CurrentPeriodAmount,
    decimal? EstimatedPeriodAmount
);

public sealed record MiscellaneousExpenseItemDto(
    MiscellaneousExpenseRowCode RowCode,
    int Category,
    InterpretativeReportRowType? RowType,
    string? Id,
    string? ItemDescription,
    decimal? PreviousPeriodAmount,
    decimal? CurrentPeriodAmount,
    decimal? EstimatedPeriodAmount
);

public sealed record InterpretativeReportDescriptionDto(
    int RowCode,
    int Category,
    InterpretativeReportRowType? RowType,
    string? Description,
    string? SectionName,
    string? AdditionalValue1,
    string? AdditionalValue2,
    string? AdditionalValue3,
    string? AdditionalValue4,
    string? AdditionalValue5
);
