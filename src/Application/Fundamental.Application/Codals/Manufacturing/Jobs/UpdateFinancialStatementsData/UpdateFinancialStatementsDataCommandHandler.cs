using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Prices.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Constants;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Prices.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;
using Polly;
using Polly.Registry;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;

public sealed class UpdateFinancialStatementsDataCommandHandler(
    IRepository repository,
    IBalanceSheetReadRepository balanceSheetReadRepository,
    IUnitOfWork unitOfWork,
    IFinancialStatementBuilder financialStatementBuilder,
    IMonthlyActivityRepository monthlyActivityRepository,
    ResiliencePipelineProvider<string> pipelineProvider
)
    : IRequestHandler<UpdateFinancialStatementsDataRequest, Response>
{
    public async Task<Response> Handle(UpdateFinancialStatementsDataRequest request, CancellationToken cancellationToken)
    {
        List<SimpleBalanceSheet> balanceSheetList = await balanceSheetReadRepository.GetLastBalanceSheetDetails(cancellationToken);
        ResiliencePipeline pipeline = pipelineProvider.GetPipeline("DbUpdateConcurrencyException");
        DateOnly today = DateTime.Now.ToDateOnly();

        foreach (SimpleBalanceSheet headerData in balanceSheetList)
        {
            Symbol? symbol = await repository.FirstOrDefaultAsync(
                new SymbolSpec().WhereIsin(headerData.Isin).ShowOfficialSymbols(true),
                cancellationToken);

            if (symbol is null)
            {
                continue;
            }

            List<BalanceSheet> balanceSheets =
                await repository.ListAsync(
                    BalanceSheetSpec.Where(
                        headerData.TraceNo,
                        headerData.FiscalYear,
                        headerData.YearEndMonth,
                        headerData.ReportMonth),
                    cancellationToken);
            List<IncomeStatement> incomeStatements =
                await repository.ListAsync(
                    IncomeStatementSpec.Where(
                        headerData.TraceNo,
                        headerData.FiscalYear,
                        headerData.YearEndMonth,
                        headerData.ReportMonth),
                    cancellationToken);
            MonthlyActivity? monthlyActivities =
                await monthlyActivityRepository.GetLastMonthlyActivity(symbol.Isin, headerData.FiscalYear, cancellationToken);

            ClosePrice? closePrice = await repository.FirstOrDefaultAsync(ClosePriceSpec.WhereLast(symbol.Isin, today), cancellationToken);

            List<IncomeStatement> lastYearIncomeStatement =
                await repository.ListAsync(
                    IncomeStatementSpec.Where(
                        headerData.TraceNo,
                        headerData.YearEndMonth.Month,
                        new FiscalYear(headerData.FiscalYear.Year - 1),
                        headerData.YearEndMonth),
                    cancellationToken);

            CodalMoney addedOperationalRevenue = CodalMoney.Empty;

            // List<Publisher> subPublishers = await repository.ListAsync(PublisherSpec.WhereParentIsin(symbol.Isin), cancellationToken);

            // foreach (Publisher thePublisher in subPublishers)
            // {
            //     SignedCodalMoney? thePublisherIncome =
            //         await incomeStatementsReadRepository.GetLastIncomeStatement(
            //             thePublisher.Symbol.Isin,
            //             headerData.FiscalYear,
            //             headerData.ReportMonth,
            //             IncomeStatementRow.OtherOperatingRevenue,
            //             cancellationToken
            //         );
            //
            //     // addedOperationalRevenue += thePublisherIncome;
            // }

            SignedCodalMoney allOperationalIncome = incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.Sales)?.Value ??
                                                    SignedCodalMoney.Empty;
            allOperationalIncome += addedOperationalRevenue;

            FinancialStatement fs = financialStatementBuilder.SetId(Guid.NewGuid())
                .SetSymbol(symbol)
                .SetCurrency(IsoCurrency.IRR)
                .SetTraceNo(headerData.TraceNo)
                .SetFiscalYear(headerData.FiscalYear)
                .SetYearEndMonth(headerData.YearEndMonth)
                .SetCreatedAt(DateTime.Now)
                .SetLastClosePrice(closePrice?.Close ?? 0, today)
                .SetMarketCap(
                    (balanceSheets
                        .FirstOrDefault(xx => xx.CodalCategory == BalanceSheetCategory.Liability && xx.CodalRow == BalanceSheetRow.Capital)
                        ?.Value.RealValue ?? 0) / IranCapitalMarket.BASE_PRICE)
                .SetIncomeStatement(
                    headerData.ReportMonth,
                    allOperationalIncome,
                    GetOtherOperationalIncome(incomeStatements),
                    GrossProfitOrLoss(incomeStatements),
                    OperationalProfitOrLoss(incomeStatements),
                    NoneOperationalProfit(incomeStatements),
                    GetCosts(incomeStatements),
                    NetProfitOrLoss(incomeStatements)
                )
                .SetSale(
                    monthlyActivities?.SaleCurrentMonth ?? CodalMoney.Empty,
                    monthlyActivities?.ReportMonth ?? StatementMonth.Empty,
                    monthlyActivities?.SaleBeforeCurrentMonth ?? CodalMoney.Empty,
                    monthlyActivities?.SaleLastYear ?? CodalMoney.Empty
                )
                .SetFinancialPosition(
                    GetAssets(balanceSheets),
                    OwnersEquity(balanceSheets),
                    Receivables(balanceSheets),
                    LastYearNetProfit(lastYearIncomeStatement)
                )
                .Build();
            repository.Add(fs);
        }

        await pipeline.ExecuteAsync(
            async _ =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            },
            cancellationToken);
        return Response.Successful();

        SignedCodalMoney GetOtherOperationalIncome(List<IncomeStatement> incomeStatements)
        {
            return incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OtherOperatingRevenue)?.Value ??
                   SignedCodalMoney.Empty;
        }

        SignedCodalMoney GrossProfitOrLoss(List<IncomeStatement> incomeStatements)
        {
            return incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.GrossProfitLoss)?.Value ?? SignedCodalMoney.Empty;
        }

        SignedCodalMoney LastYearNetProfit(List<IncomeStatement> lastYearIncomeStatement)
        {
            return lastYearIncomeStatement.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.NetIncomeLoss)?.Value ??
                   SignedCodalMoney.Empty;
        }
    }

    private static decimal Receivables(List<BalanceSheet> balanceSheets)
    {
        return balanceSheets.FirstOrDefault(x =>
                x.CodalCategory == BalanceSheetCategory.Assets && x.CodalRow == BalanceSheetRow.TradeAndOtherReceivables)?.Value
            .Value ?? CodalMoney.Empty;
    }

    private static decimal OwnersEquity(List<BalanceSheet> balanceSheets)
    {
        return balanceSheets.FirstOrDefault(x =>
                   x.CodalCategory == BalanceSheetCategory.Liability && x.CodalRow == BalanceSheetRow.TotalEquity)?.Value.Value ??
               CodalMoney.Empty;
    }

    private static decimal GetAssets(List<BalanceSheet> balanceSheets)
    {
        return balanceSheets.FirstOrDefault(x =>
                   x.CodalCategory == BalanceSheetCategory.Assets && x.CodalRow == BalanceSheetRow.TotalAssets)?.Value.Value ??
               CodalMoney.Empty;
    }

    private static SignedCodalMoney NetProfitOrLoss(List<IncomeStatement> incomeStatements)
    {
        return incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.NetIncomeLoss)?.Value ?? SignedCodalMoney.Empty;
    }

    private static decimal GetCosts(List<IncomeStatement> incomeStatements)
    {
        return incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OtherNoneOperationalIncomeOrExpense)?.Value
            .Value ?? CodalMoney.Empty;
    }

    private static SignedCodalMoney NoneOperationalProfit(List<IncomeStatement> incomeStatements)
    {
        return incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OtherNoneOperationalIncomeOrExpense)?.Value ??
               SignedCodalMoney.Empty;
    }

    private static SignedCodalMoney OperationalProfitOrLoss(List<IncomeStatement> incomeStatements)
    {
        return incomeStatements.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OperatingProfitLoss)?.Value ??
               SignedCodalMoney.Empty;
    }
}