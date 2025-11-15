using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class InterpretativeReportSummaryPage5DetailItemSpec : Specification<CanonicalInterpretativeReportSummaryPage5, GetInterpretativeReportSummaryPage5DetailItem>
{
    public InterpretativeReportSummaryPage5DetailItemSpec()
    {
        Query
            .AsNoTracking()
            .AsSplitQuery()
            .Select(x => new GetInterpretativeReportSummaryPage5DetailItem(
                x.Id,
                x.Symbol.Isin,
                x.Symbol.Name,
                x.Symbol.Title,
                x.Uri,
                x.Version,
                x.FiscalYear.Year,
                x.YearEndMonth.Month,
                x.ReportMonth.Month,
                x.TraceNo,
                x.PublishDate,
                x.OtherOperatingIncomes.Select(item => new OtherOperatingIncomeItemDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.Id,
                    item.ItemDescription,
                    item.PreviousPeriodAmount,
                    item.CurrentPeriodAmount,
                    item.EstimatedPeriodAmount
                )).ToList(),
                x.OtherNonOperatingExpenses.Select(item => new OtherNonOperatingExpenseItemDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.Id,
                    item.ItemDescription,
                    item.PreviousPeriodAmount,
                    item.CurrentPeriodAmount,
                    item.EstimatedPeriodAmount
                )).ToList(),
                x.FinancingDetails.Select(item => new FinancingDetailItemDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.FinancingSource,
                    item.InterestRate,
                    item.BeginningBalance,
                    item.EndingBalanceRial,
                    item.CurrencyType,
                    item.ForeignAmount,
                    item.ForeignRialEquivalent,
                    item.ShortTerm,
                    item.LongTerm,
                    item.FinancialExpense,
                    item.OtherDescriptions
                )).ToList(),
                x.FinancingDetailsEstimated.Select(item => new FinancingDetailItemDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.FinancingSource,
                    item.InterestRate,
                    item.BeginningBalance,
                    item.EndingBalanceRial,
                    item.CurrencyType,
                    item.ForeignAmount,
                    item.ForeignRialEquivalent,
                    item.ShortTerm,
                    item.LongTerm,
                    item.FinancialExpense,
                    item.OtherDescriptions
                )).ToList(),
                x.InvestmentIncomes.Select(item => new InvestmentIncomeItemDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.Id,
                    item.ItemDescription,
                    item.PreviousPeriodAmount,
                    item.CurrentPeriodAmount,
                    item.EstimatedPeriodAmount
                )).ToList(),
                x.MiscellaneousExpenses.Select(item => new MiscellaneousExpenseItemDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.Id,
                    item.ItemDescription,
                    item.PreviousPeriodAmount,
                    item.CurrentPeriodAmount,
                    item.EstimatedPeriodAmount
                )).ToList(),
                x.Descriptions.Select(item => new InterpretativeReportDescriptionDto(
                    item.RowCode,
                    item.Category,
                    item.RowType,
                    item.Description,
                    item.SectionName,
                    item.AdditionalValue1,
                    item.AdditionalValue2,
                    item.AdditionalValue3,
                    item.AdditionalValue4,
                    item.AdditionalValue5
                )).ToList()
            ));
    }

    public InterpretativeReportSummaryPage5DetailItemSpec WhereId(Guid id)
    {
        Query.Where(x => x.Id == id);
        return this;
    }
}
