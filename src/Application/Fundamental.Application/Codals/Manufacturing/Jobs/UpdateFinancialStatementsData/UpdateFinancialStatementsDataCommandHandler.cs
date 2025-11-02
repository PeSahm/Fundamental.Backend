using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
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
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;

public sealed class UpdateFinancialStatementsDataCommandHandler(
    IRepository repository,
    IUnitOfWork unitOfWork,
    IFinancialStatementBuilder financialStatementBuilder,
    IMonthlyActivityRepository monthlyActivityRepository,
    IBalanceSheetReadRepository balanceSheetReadRepository,
    IIncomeStatementsReadRepository incomeStatementsReadRepository,
    ICodalService codalService,
    ResiliencePipelineProvider<string> pipelineProvider,
    ILogger<UpdateFinancialStatementsDataCommandHandler> logger
)
    : IRequestHandler<UpdateFinancialStatementsDataRequest, Response>
{
    public async Task<Response> Handle(UpdateFinancialStatementsDataRequest request, CancellationToken cancellationToken)
    {
        List<SimpleBalanceSheet> balanceSheetList = await balanceSheetReadRepository.GetLastBalanceSheetDetails(cancellationToken);
        balanceSheetList = balanceSheetList.OrderBy(x => x.FiscalYear.Year).ThenBy(x => x.ReportMonth.Month).ToList();
        ResiliencePipeline pipeline = pipelineProvider.GetPipeline("DbUpdateConcurrencyException");
        DateOnly today = DateTime.Now.ToDateOnly();

        foreach (SimpleBalanceSheet headerData in balanceSheetList)
        {
            try
            {
                Symbol? symbol = await repository.FirstOrDefaultAsync(
                    new SymbolSpec().WhereIsin(headerData.Isin).ShowOfficialSymbols(true),
                    cancellationToken);

                if (symbol is null)
                {
                    continue;
                }

                List<BalanceSheetDetail> balanceSheetDetails = await repository.ListAsync(
                    BalanceSheetDetailEntitySpec.WhereBalanceSheet(headerData.TraceNo, headerData.FiscalYear, headerData.ReportMonth),
                    cancellationToken);
                IncomeStatement? incomeStatement =
                    await repository.FirstOrDefaultAsync(
                        IncomeStatementSpec.Where(
                            headerData.TraceNo,
                            headerData.FiscalYear,
                            headerData.YearEndMonth,
                            headerData.ReportMonth),
                        cancellationToken);
                // Note: MonthlyActivity entity removed - now using CanonicalMonthlyActivity with rich collections
                // CanonicalMonthlyActivity? monthlyActivities =
                //     await monthlyActivityRepository.GetFirstMonthlyActivity(
                //         symbol.Isin,
                //         headerData.FiscalYear,
                //         headerData.ReportMonth,
                //         cancellationToken);
                // CanonicalMonthlyActivity? monthlyActivitiesLastYearSamePeriod =
                //     await monthlyActivityRepository.GetMonthlyActivity(
                //         symbol.Isin,
                //         headerData.FiscalYear - 1,
                //         headerData.ReportMonth,
                //         cancellationToken);

                // Note: ApplyExtraSale() was specific to old MonthlyActivity entity
                // TODO: If extra sales calculation needed, implement using CanonicalMonthlyActivity collections
                // monthlyActivitiesLastYearSamePeriod?.ApplyExtraSale();
                // monthlyActivities?.ApplyExtraSale();

                GetStatementResponse? theStatement = await codalService.GetStatementByTraceNo(headerData.TraceNo, cancellationToken);
                ClosePrice? closePrice =
                    await repository.FirstOrDefaultAsync(
                        ClosePriceSpec.WhereLast(symbol.Isin, theStatement?.PublishDateMiladi.ToDateOnly() ?? today),
                        cancellationToken);

                SignedCodalMoney? lastYearSamePeriodNetProfit =
                    await incomeStatementsReadRepository.GetLastIncomeStatement(
                        headerData.Isin,
                        headerData.FiscalYear - 1,
                        headerData.ReportMonth,
                        IncomeStatementRow.NetIncomeLoss,
                        cancellationToken);

                SignedCodalMoney? lastYearSamePeriodCostOfGoodsSale =
                    await incomeStatementsReadRepository.GetLastIncomeStatement(
                        headerData.Isin,
                        headerData.FiscalYear - 1,
                        headerData.ReportMonth,
                        IncomeStatementRow.CostOfGoodsSale,
                        cancellationToken);

                SignedCodalMoney? lastYearFullPeriodCostOfGoodsSale =
                    await incomeStatementsReadRepository.GetLastIncomeStatement(
                        headerData.Isin,
                        headerData.FiscalYear - 1,
                        headerData.YearEndMonth,
                        IncomeStatementRow.CostOfGoodsSale,
                        cancellationToken);
                SignedCodalMoney? lastYearSamePeriodInventory = await balanceSheetReadRepository.GetLastBalanceSheetItem(
                    headerData.Isin,
                    headerData.FiscalYear - 1,
                    headerData.ReportMonth,
                    BalanceSheetCategory.Assets,
                    BalanceSheetRow.Inventory,
                    cancellationToken);

                SignedCodalMoney? lastYearSamePeriodOperationalIncome =
                    await incomeStatementsReadRepository.GetLastIncomeStatement(
                        headerData.Isin,
                        headerData.FiscalYear - 1,
                        headerData.ReportMonth,
                        IncomeStatementRow.Sales,
                        cancellationToken);

                SignedCodalMoney? lastYearFullPeriodOperationalIncome =
                    await incomeStatementsReadRepository.GetLastIncomeStatement(
                        headerData.Isin,
                        headerData.FiscalYear - 1,
                        headerData.YearEndMonth,
                        IncomeStatementRow.Sales,
                        cancellationToken);
                SignedCodalMoney? lastYearSamePeriodTradeAndOtherReceivables = await balanceSheetReadRepository.GetLastBalanceSheetItem(
                    headerData.Isin,
                    headerData.FiscalYear.Year - 1,
                    headerData.ReportMonth,
                    BalanceSheetCategory.Assets,
                    BalanceSheetRow.TradeAndOtherReceivables,
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

                if (incomeStatement is null)
                {
                    continue;
                }

                SignedCodalMoney allOperationalIncome = incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.Sales)?.Value ??
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
                        (balanceSheetDetails
                            .FirstOrDefault(xx => xx.CodalCategory == BalanceSheetCategory.Liability && xx.CodalRow == BalanceSheetRow.Capital)
                            ?.Value.RealValue ?? 0) / IranCapitalMarket.BASE_PRICE)
                    .SetIncomeStatement(
                        headerData.ReportMonth,
                        allOperationalIncome,
                        GetOtherOperationalIncome(incomeStatement),
                        GrossProfitOrLoss(incomeStatement),
                        OperationalProfitOrLoss(incomeStatement),
                        NoneOperationalProfit(incomeStatement),
                        GetCosts(incomeStatement),
                        NetProfitOrLoss(incomeStatement)
                    )
                    .SetSale(
                        CodalMoney.Empty, // monthlyActivities?.SaleCurrentMonth - TODO: Calculate from CanonicalMonthlyActivity
                        StatementMonth.Empty, // monthlyActivities?.ReportMonth
                        0, // monthlyActivities?.TraceNo
                        FiscalYear.Empty, // monthlyActivities?.FiscalYear
                        CodalMoney.Empty, // monthlyActivities?.SaleBeforeCurrentMonth
                        CodalMoney.Empty // monthlyActivitiesLastYearSamePeriod?.SaleIncludeCurrentMonth
                    )
                    .SetFinancialPosition(
                        GetAssets(balanceSheetDetails),
                        OwnersEquity(balanceSheetDetails),
                        Receivables(balanceSheetDetails),
                        lastYearSamePeriodNetProfit ?? SignedCodalMoney.Empty
                    )
                    .SetInventoryOutstanding(new FinancialStatement.DaysInventoryOutstanding
                        {
                            LastYearSamePeriodCostOfGoodsSale = lastYearSamePeriodCostOfGoodsSale ?? SignedCodalMoney.Empty,
                            LastYearFullPeriodCostOfGoodsSale = lastYearFullPeriodCostOfGoodsSale ?? SignedCodalMoney.Empty,
                            CurrentCostOfGoodsSale = incomeStatement.Details.First(x => x.CodalRow == IncomeStatementRow.CostOfGoodsSale)
                                .Value,
                            LastYearSamePeriodInventory = lastYearSamePeriodInventory ?? SignedCodalMoney.Empty,
                            CurrentInventory = balanceSheetDetails.First(x => x.CodalCategory == BalanceSheetCategory.Assets
                                                                        && x.CodalRow == BalanceSheetRow.Inventory).Value,
                        }
                    )
                    .SetDaysSalesOutstanding(new FinancialStatement.SalesOutstanding
                        {
                            LastYearSamePeriodOperationalIncome = lastYearSamePeriodOperationalIncome ?? SignedCodalMoney.Empty,
                            LastYearFullPeriodOperationalIncome = lastYearFullPeriodOperationalIncome ?? SignedCodalMoney.Empty,
                            CurrentOperationalIncome = incomeStatement.Details.First(x => x.CodalRow == IncomeStatementRow.Sales)
                                .Value,
                            LastYearSamePeriodTradeAndOtherReceivables =
                                lastYearSamePeriodTradeAndOtherReceivables ?? SignedCodalMoney.Empty,
                            CurrentTradeAndOtherReceivables =
                                balanceSheetDetails.First(x => x.CodalCategory == BalanceSheetCategory.Assets
                                                         && x.CodalRow == BalanceSheetRow.TradeAndOtherReceivables).Value,
                        }
                    )
                    .Build();
                repository.Add(fs);
            }
            catch (Exception e)
            {
                logger.LogWarning(e, "Error processing financial statement for  {@HeaderData}: {Error}", headerData, e.Message);
            }
        }

        await pipeline.ExecuteAsync(
            async _ =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            },
            cancellationToken);

        return Response.Successful();

        SignedCodalMoney GetOtherOperationalIncome(IncomeStatement incomeStatement)
        {
            return incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OtherOperatingRevenue)?.Value ??
                   SignedCodalMoney.Empty;
        }

        SignedCodalMoney GrossProfitOrLoss(IncomeStatement incomeStatement)
        {
            return incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.GrossProfitLoss)?.Value ?? SignedCodalMoney.Empty;
        }
    }

    private static decimal Receivables(List<BalanceSheetDetail> balanceSheetDetails)
    {
        return balanceSheetDetails.FirstOrDefault(x =>
                x.CodalCategory == BalanceSheetCategory.Assets && x.CodalRow == BalanceSheetRow.TradeAndOtherReceivables)?.Value
            .Value ?? CodalMoney.Empty;
    }

    private static decimal OwnersEquity(List<BalanceSheetDetail> balanceSheetDetails)
    {
        return balanceSheetDetails.FirstOrDefault(x =>
                   x.CodalCategory == BalanceSheetCategory.Liability && x.CodalRow == BalanceSheetRow.TotalEquity)?.Value.Value ??
               CodalMoney.Empty;
    }

    private static decimal GetAssets(List<BalanceSheetDetail> balanceSheetDetails)
    {
        return balanceSheetDetails.FirstOrDefault(x =>
                   x.CodalCategory == BalanceSheetCategory.Assets && x.CodalRow == BalanceSheetRow.TotalAssets)?.Value.Value ??
               CodalMoney.Empty;
    }

    private static SignedCodalMoney NetProfitOrLoss(IncomeStatement incomeStatement)
    {
        return incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.NetIncomeLoss)?.Value ?? SignedCodalMoney.Empty;
    }

    private static decimal GetCosts(IncomeStatement incomeStatement)
    {
        return incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OtherNoneOperationalIncomeOrExpense)?.Value
            .Value ?? CodalMoney.Empty;
    }

    private static SignedCodalMoney NoneOperationalProfit(IncomeStatement incomeStatement)
    {
        return incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OtherNoneOperationalIncomeOrExpense)?.Value ??
               SignedCodalMoney.Empty;
    }

    private static SignedCodalMoney OperationalProfitOrLoss(IncomeStatement incomeStatement)
    {
        return incomeStatement.Details.FirstOrDefault(x => x.CodalRow == IncomeStatementRow.OperatingProfitLoss)?.Value ??
               SignedCodalMoney.Empty;
    }
}