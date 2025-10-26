using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.BalanceSheets.V5;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.BalanceSheets;

public sealed class BalanceSheetV5Processor(IServiceScopeFactory serviceScopeFactory, ILogger<BalanceSheetV5Processor> logger)
    : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.InterimStatement;
    public static CodalVersion CodalVersion => CodalVersion.V5;
    public static LetterPart LetterPart => LetterPart.BalanceSheet;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;
        JObject jObject = JObject.Parse(model.Json);

        // List of properties to keep
        HashSet<string> propertiesToKeep =
        [
            "listedCapital",
            "unauthorizedCapital",
            "balanceSheet"
        ];

        List<JProperty> properties = jObject.Properties().ToList();

        properties
            .Where(p => !propertiesToKeep.Contains(p.Name))
            .ToList()
            .ForEach(p => p.Remove());

        // Deserialize the filtered JSON into your model
        RootCodalBalanceSheet? codalBalanceSheetRoot = jObject.ToObject<RootCodalBalanceSheet>(new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        if (codalBalanceSheetRoot?.BalanceSheetData is null)
        {
            return;
        }

        if (!codalBalanceSheetRoot.IsValidReport())
        {
            return;
        }

        BalanceSheetDto balanceSheetDto = codalBalanceSheetRoot.BalanceSheetData.BalanceSheet;
        balanceSheetDto.AddCustomRowItems();

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        foreach (YearDatum yearDatum in balanceSheetDto.YearData)
        {
            try
            {
                // Additional validation of date values
                if (yearDatum.FiscalYear is null || yearDatum.FiscalMonth is null || yearDatum.ReportMonth is null)
                {
                    continue;
                }

                Symbol symbol =
                    await dbContext.Symbols.FirstAsync(
                        x => x.Isin == statement.Isin,
                        cancellationToken);

                foreach (RowItem rowItem in balanceSheetDto.RowItems)
                {
                    try
                    {
                        BalanceSheet balanceSheet = new(
                            id: Guid.NewGuid(),
                            symbol: symbol,
                            traceNo: statement.TracingNo,
                            uri: statement.HtmlUrl,
                            fiscalYear: new FiscalYear(yearDatum.FiscalYear.Value),
                            yearEndMonth: new StatementMonth(yearDatum.FiscalMonth.Value),
                            reportMonth: new StatementMonth(yearDatum.ReportMonth.Value),
                            row: rowItem.RowNumber,
                            codalRow: (ushort)rowItem.RowCode,
                            rowItem.Category == Category.Assets ? BalanceSheetCategory.Assets : BalanceSheetCategory.Liability,
                            description: rowItem.GetDescription(),
                            value: rowItem.GetValue(yearDatum.ColumnId),
                            isAudited: yearDatum.IsAudited,
                            createdAt: DateTime.Now,
                            publishDate: statement.PublishDateMiladi.ToUniversalTime()
                        );

                        if (await dbContext.BalanceSheets
                                .AnyAsync(
                                    x =>
                                        x.Symbol.Isin == balanceSheet.Symbol.Isin &&
                                        x.FiscalYear.Year == balanceSheet.FiscalYear &&
                                        x.ReportMonth.Month == balanceSheet.ReportMonth &&
                                        x.CodalRow == balanceSheet.CodalRow &&
                                        x.CodalCategory == balanceSheet.CodalCategory &&
                                        x.TraceNo == balanceSheet.TraceNo,
                                    cancellationToken))
                        {
                            continue;
                        }

                        dbContext.BalanceSheets.Add(balanceSheet);
                        logger.LogDebug(
                            "Created balance sheet entry for trace no {TraceNo}, row {RowNumber}, description {Description}",
                            statement.TracingNo,
                            rowItem.RowNumber,
                            rowItem.GetDescription());
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(
                            ex,
                            "Error processing row item for trace no {TraceNo}, row number {RowNumber}. Error: {Error}",
                            statement.TracingNo,
                            rowItem.RowNumber,
                            ex.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(
                    ex,
                    "Error processing year datum for trace no {TraceNo}, fiscal year {FiscalYear}. Error: {Error}",
                    statement.TracingNo,
                    yearDatum.FiscalYear,
                    ex.Message);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Successfully processed balance sheet for trace no {TraceNo}", statement.TracingNo);
    }
}