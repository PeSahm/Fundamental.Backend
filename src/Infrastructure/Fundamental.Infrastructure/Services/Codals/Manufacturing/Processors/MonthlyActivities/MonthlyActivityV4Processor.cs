using Fundamental.Application.Codals.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.BuildingBlock;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Constants;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

public class MonthlyActivityV4Processor(
    IServiceScopeFactory serviceScopeFactory
)
    : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;

    public static CodalVersion CodalVersion => CodalVersion.V4;

    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;
        CodalMonthlyActivity? saleDate =
            JsonConvert.DeserializeObject<CodalMonthlyActivity>(model.Json, setting);

        if (saleDate is null)
        {
            return;
        }

        if (saleDate.MonthlyActivity is null)
        {
            return;
        }

        if (saleDate.MonthlyActivity.ProductionAndSales is null)
        {
            return;
        }

        if (saleDate.MonthlyActivity.ProductionAndSales.YearData.Count == 0)
        {
            return;
        }

        YearDatum? yearDatum = saleDate.MonthlyActivity.ProductionAndSales.YearData
            .Find(x => x.ColumnId == SaleColumnId.SaleThisMonth);

        if (yearDatum is null)
        {
            return;
        }

        if (yearDatum.FiscalYear is null || yearDatum.FiscalMonth is null || yearDatum.ReportMonth is null)
        {
            return;
        }

        SymbolExtensions.SalesInfo extraInfo = new();

        if (statement.Isin != null && statement.Isin.Equals(IranCapitalMarket.FezarIsin, StringComparison.OrdinalIgnoreCase))
        {
            extraInfo =
                SymbolExtensions.ExtractFezarInfo(
                    string.Join(
                        ',',
                        saleDate.MonthlyActivity.ProductMonthlyActivityDesc1
                            .RowItems.Select(x => x.Value11991).ToList()
                    )
                );
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        MonthlyActivity? existingStatement = await dbContext.MonthlyActivities
            .FirstOrDefaultAsync(
                x =>
                    x.Symbol.Isin == statement.Isin && x.FiscalYear.Year == yearDatum.FiscalYear &&
                    x.ReportMonth.Month == yearDatum.ReportMonth,
                cancellationToken);
        Symbol symbol =
            await dbContext.Symbols.FirstAsync(
                x => x.Isin == statement.Isin,
                cancellationToken);

        if (existingStatement == null)
        {
            MonthlyActivity monthlyActivity = new(
                Guid.NewGuid(),
                symbol,
                statement.TracingNo,
                statement.HtmlUrl,
                yearDatum.FiscalYear,
                yearDatum.FiscalMonth.Value,
                yearDatum.ReportMonth.Value,
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountExclusiveThisMonth),
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountThisMonth),
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountInclusiveThisMonth),
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountPrevYear),
                false,
                [extraInfo],
                DateTime.Now
            );
            dbContext.Add(monthlyActivity);
        }
        else
        {
            if (existingStatement.TraceNo < statement.TracingNo)
            {
                existingStatement.Update(
                    symbol,
                    statement.TracingNo,
                    statement.HtmlUrl,
                    yearDatum.FiscalYear,
                    yearDatum.FiscalMonth.Value,
                    yearDatum.ReportMonth.Value,
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountExclusiveThisMonth),
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountThisMonth),
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountInclusiveThisMonth),
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountPrevYear),
                    false,
                    [extraInfo],
                    DateTime.Now
                );
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private RowItem GetSumRecord(CodalMonthlyActivity saleDate)
    {
        return saleDate.MonthlyActivity!.ProductionAndSales!.RowItems.First(x =>
            x is { RowCode: RowCode.TotalSum, Category: Category.Sum });
    }
}