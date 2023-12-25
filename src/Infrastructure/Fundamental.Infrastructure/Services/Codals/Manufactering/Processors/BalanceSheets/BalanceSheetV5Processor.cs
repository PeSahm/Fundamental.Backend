using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.BalanceSheets.V5;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Enums;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufactering.Processors.BalanceSheets;

public sealed class BalanceSheetV5Processor(IServiceScopeFactory serviceScopeFactory) : ICodalProcessor
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
            Symbol symbol =
                await dbContext.Symbols.FirstAsync(
                    predicate: x => x.Isin == statement.Isin,
                    cancellationToken: cancellationToken);

            if (await dbContext.BalanceSheets
                    .AnyAsync(
                        x =>
                            x.Symbol.Isin == statement.Isin &&
                            x.FiscalYear.Year == yearDatum.FiscalYear &&
                            x.ReportMonth.Month == yearDatum.ReportMonth &&
                            x.TraceNo == statement.TracingNo,
                        cancellationToken: cancellationToken))
            {
                continue;
            }

            foreach (RowItem rowItem in balanceSheetDto.RowItems)
            {
                BalanceSheet balanceSheet = new(
                    Guid.NewGuid(),
                    symbol,
                    statement.TracingNo,
                    statement.HtmlUrl,
                    fiscalYear: yearDatum.FiscalYear!,
                    yearEndMonth: yearDatum.FiscalMonth!.Value,
                    reportMonth: yearDatum.ReportMonth!.Value,
                    row: rowItem.RowNumber,
                    (ushort)rowItem.RowCode,
                    rowItem.Category == Category.Assets ? BalanceSheetCategory.Assets : BalanceSheetCategory.Liability,
                    rowItem.GetDescription(),
                    rowItem.GetValue(yearDatum.ColumnId),
                    yearDatum.IsAudited,
                    DateTime.Now
                );
                dbContext.BalanceSheets.Add(balanceSheet);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}