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

        foreach (JProperty property in properties)
        {
            if (!propertiesToKeep.Contains(property.Name))
            {
                property.Remove();
            }
        }

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

                if (await dbContext.BalanceSheets
                        .AnyAsync(
                            x =>
                                x.Symbol.Isin == statement.Isin &&
                                x.FiscalYear.Year == yearDatum.FiscalYear.Value &&
                                x.ReportMonth.Month == yearDatum.ReportMonth.Value &&
                                x.TraceNo == statement.TracingNo,
                            cancellationToken))
                {
                    continue;
                }

                foreach (RowItem rowItem in balanceSheetDto.RowItems)
                {
                    try
                    {
                        BalanceSheet balanceSheet = new(
                            Guid.NewGuid(),
                            symbol,
                            statement.TracingNo,
                            statement.HtmlUrl,
                            new FiscalYear(yearDatum.FiscalYear.Value),
                            new StatementMonth(yearDatum.FiscalMonth.Value),
                            new StatementMonth(yearDatum.ReportMonth.Value),
                            rowItem.RowNumber,
                            (ushort)rowItem.RowCode,
                            rowItem.Category == Category.Assets ? BalanceSheetCategory.Assets : BalanceSheetCategory.Liability,
                            rowItem.GetDescription(),
                            rowItem.GetValue(yearDatum.ColumnId),
                            yearDatum.IsAudited,
                            DateTime.Now
                        );
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