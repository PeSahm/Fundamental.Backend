using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementList;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class FinancialStatementReadRepository(FundamentalDbContext dataContent) : IFinancialStatementReadRepository
{
    public async Task<FinancialStatement?> GetLastFinancialStatement(
        string isin,
        DateOnly date,
        CancellationToken cancellationToken = default
    )
    {
        int persianYear = date.GetPersianYear();

        return await dataContent.ManufacturingFinancialStatement
            .Where(x => x.Symbol.Isin == isin && x.FiscalYear.Year <= persianYear)
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(x => x.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<FinancialStatement?> GetLastFinancialStatement(
        string isin,
        FiscalYear year,
        StatementMonth month,
        CancellationToken cancellationToken = default
    )
    {
        return dataContent.ManufacturingFinancialStatement
            .Where(x => x.Symbol.Isin == isin &&
                        (x.FiscalYear.Year < year.Year || (x.FiscalYear.Year == year.Year && x.ReportMonth.Month <= month.Month)))
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(x => x.TraceNo)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<Response<Paginated<GetFinancialStatementsResultDto>>> GetLastFinancialStatementList(
        GetFinancialStatementListRequest request,
        CancellationToken cancellationToken = default
    )
    {
        string[] isinList = request.IsinList ?? [];
        Paginated<GetFinancialStatementsResultDto> query = await dataContent.ManufacturingFinancialStatement
            .AsNoTracking()
            .Include(x => x.Symbol)
            .Where(x => !isinList.Any() || isinList.Contains(x.Symbol.Isin))

            //.Where(x => request.SectorCode == null || (x.Symbol.Sector != null && x.Symbol.Sector.Id == request.SectorCode))
            .Where(x => x.LastClosePrice > 0)
            .Where(x => x.Pe > 0)
            .Where(x =>
                !request.MarketValueRange.HasValue ||
                !request.MarketValueRange.Value.IsValid() ||
                (x.MarketValue >= request.MarketValueRange.Value.From && x.MarketValue <= request.MarketValueRange.Value.To))
            .Where(x => x.Id == dataContent.ManufacturingFinancialStatement
                .Where(xx => xx.Symbol.Isin == x.Symbol.Isin)
                .OrderByDescending(xx => xx.FiscalYear.Year)
                .ThenByDescending(xx => xx.ReportMonth.Month)
                .ThenByDescending(xx => xx.TraceNo)
                .Select(xx => xx.Id)
                .FirstOrDefault())
            .Select(x => new GetFinancialStatementsResultDto
            {
                Isin = x.Symbol.Isin,
                Name = x.Symbol.Name,
                TraceNo = x.TraceNo,
                FiscalYear = x.FiscalYear,
                ReportMonth = x.ReportMonth,
                OperationalIncome = x.OperationalIncome,
                GrossProfitOrLoss = x.GrossProfitOrLoss,
                OperationalProfitOrLoss = x.OperationalProfitOrLoss,
                NetProfitOrLoss = x.NetProfitOrLoss,
                OtherOperationalIncome = x.OtherOperationalIncome,
                NoneOperationalProfit = x.NoneOperationalProfit,
                Sale = x.Sale,
                SaleBeforeThisMonth = x.SaleBeforeThisMonth,
                Assets = x.Assets,
                MarketValue = x.MarketValue,
                OwnersEquity = x.OwnersEquity,
                LastClosePrice = x.LastClosePrice,
                MarketCap = x.MarketCap,
                Pa = x.Pa,
                Pe = x.Pe,
                Receivables = x.Receivables,
                NetMargin = x.NetMargin,
                Pb = x.Pb,
                Ps = x.Ps,
                ForecastSale = x.ForecastSale,
                ReceivableRatio = x.ReceivableRatio,
                ForecastTotalProfit = x.ForecastTotalProfit,
                OwnersEquityRatio = x.OwnersEquityRatio,
                ThisPeriodSaleRatio = x.ThisPeriodSaleRatio,
                SaleAverageExcludeThisPeriod = x.SaleAverageExcludeThisPeriod,
                SaleAverageLastYearSamePeriod = x.SaleAverageLastYearSamePeriod,
                ThisPeriodSaleRatioWithLastYear = x.ThisPeriodSaleRatioWithLastYear,
                TseInsCode = x.Symbol.TseInsCode,
                InventoryOutstandingData = x.InventoryOutstandingData,
                SalesOutstandingData = x.SalesOutstandingData,
            }).ToPagingListAsync(request, "MarketValue desc", cancellationToken);

        return query;
    }
}