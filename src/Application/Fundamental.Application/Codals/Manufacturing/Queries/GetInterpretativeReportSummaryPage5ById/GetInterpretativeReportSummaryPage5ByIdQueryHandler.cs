using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;

public sealed class GetInterpretativeReportSummaryPage5ByIdQueryHandler(
    FundamentalDbContext dbContext
) : IRequestHandler<GetInterpretativeReportSummaryPage5ByIdRequest, Response<GetInterpretativeReportSummaryPage5DetailItem>>
{
    public async Task<Response<GetInterpretativeReportSummaryPage5DetailItem>> Handle(
        GetInterpretativeReportSummaryPage5ByIdRequest request,
        CancellationToken cancellationToken)
    {
        var entity = await dbContext.CanonicalInterpretativeReportSummaryPage5s
            .AsNoTracking()
            .Include(x => x.Symbol)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

        if (entity == null)
        {
            return GetInterpretativeReportSummaryPage5ByIdErrorCodes.InterpretativeReportSummaryPage5NotFound;
        }

        return Response.Successful(new GetInterpretativeReportSummaryPage5DetailItem(
            entity.Id,
            entity.Symbol.Isin,
            entity.Symbol.SymbolName,
            entity.Symbol.Title,
            entity.Uri,
            entity.Version,
            entity.FiscalYear.Year,
            entity.YearEndMonth.Month,
            entity.ReportMonth.Month,
            entity.TraceNo,
            entity.PublishDate,
            entity.OtherOperatingIncomes.Select(x => new OtherOperatingIncomeItemDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.Id,
                x.ItemDescription,
                x.PreviousPeriodAmount,
                x.CurrentPeriodAmount,
                x.EstimatedPeriodAmount
            )).ToList(),
            entity.OtherNonOperatingExpenses.Select(x => new OtherNonOperatingExpenseItemDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.Id,
                x.ItemDescription,
                x.PreviousPeriodAmount,
                x.CurrentPeriodAmount,
                x.EstimatedPeriodAmount
            )).ToList(),
            entity.FinancingDetails.Select(x => new FinancingDetailItemDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.FinancingSource,
                x.InterestRate,
                x.BeginningBalance,
                x.EndingBalanceRial,
                x.CurrencyType,
                x.ForeignAmount,
                x.ForeignRialEquivalent,
                x.ShortTerm,
                x.LongTerm,
                x.FinancialExpense,
                x.OtherDescriptions
            )).ToList(),
            entity.FinancingDetailsEstimated.Select(x => new FinancingDetailItemDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.FinancingSource,
                x.InterestRate,
                x.BeginningBalance,
                x.EndingBalanceRial,
                x.CurrencyType,
                x.ForeignAmount,
                x.ForeignRialEquivalent,
                x.ShortTerm,
                x.LongTerm,
                x.FinancialExpense,
                x.OtherDescriptions
            )).ToList(),
            entity.InvestmentIncomes.Select(x => new InvestmentIncomeItemDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.Id,
                x.ItemDescription,
                x.PreviousPeriodAmount,
                x.CurrentPeriodAmount,
                x.EstimatedPeriodAmount
            )).ToList(),
            entity.MiscellaneousExpenses.Select(x => new MiscellaneousExpenseItemDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.Id,
                x.ItemDescription,
                x.PreviousPeriodAmount,
                x.CurrentPeriodAmount,
                x.EstimatedPeriodAmount
            )).ToList(),
            entity.Descriptions.Select(x => new InterpretativeReportDescriptionDto(
                x.RowCode,
                x.Category,
                x.RowType,
                x.Description,
                x.SectionName,
                x.AdditionalValue1,
                x.AdditionalValue2,
                x.AdditionalValue3,
                x.AdditionalValue4,
                x.AdditionalValue5
            )).ToList()
        ));
    }
}
