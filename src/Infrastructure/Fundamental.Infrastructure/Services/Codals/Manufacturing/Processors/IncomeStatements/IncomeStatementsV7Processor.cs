using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.IncomeStatements.V7;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.IncomeStatements;

public sealed class IncomeStatementsV7Processor(IServiceScopeFactory serviceScopeFactory) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.InterimStatement;
    public static CodalVersion CodalVersion => CodalVersion.V7;
    public static LetterPart LetterPart => LetterPart.IncomeStatement;

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
            "incomeStatement"
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
        RootCodalIncomeStatement? codalBalanceSheetRoot = jObject.ToObject<RootCodalIncomeStatement>(new JsonSerializer
        {
            NullValueHandling = NullValueHandling.Ignore
        });

        if (codalBalanceSheetRoot?.CodalIncomeStatement is null)
        {
            return;
        }

        if (!codalBalanceSheetRoot.IsValidReport())
        {
            return;
        }

        IncomeStatementDto incomeStatementDto = codalBalanceSheetRoot.CodalIncomeStatement.IncomeStatement;

        incomeStatementDto.AddCustomRowItems();

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        foreach (YearDatum yearDatum in incomeStatementDto.YearData)
        {
            Symbol symbol =
                await dbContext.Symbols.FirstAsync(
                    predicate: x => x.Isin == statement.Isin,
                    cancellationToken: cancellationToken);

            if (await dbContext.IncomeStatements
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

            foreach (RowItem rowItem in incomeStatementDto.RowItems)
            {
                IncomeStatement incomeStatement = new(
                    Guid.NewGuid(),
                    symbol,
                    statement.TracingNo,
                    statement.HtmlUrl,
                    fiscalYear: yearDatum.FiscalYear!,
                    yearEndMonth: yearDatum.FiscalMonth!.Value,
                    reportMonth: yearDatum.ReportMonth!.Value,
                    row: rowItem.RowNumber,
                    rowItem.RowCode,
                    rowItem.GetDescription(),
                    rowItem.GetValue(yearDatum.ColumnId),
                    yearDatum.IsAudited,
                    DateTime.Now
                );
                dbContext.IncomeStatements.Add(incomeStatement);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}