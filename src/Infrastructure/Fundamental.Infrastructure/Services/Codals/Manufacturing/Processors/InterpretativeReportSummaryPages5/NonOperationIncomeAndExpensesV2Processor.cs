using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    NonOperationIncomeAndExpenses;
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

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages5;

public class NonOperationIncomeAndExpensesV2Processor(IServiceScopeFactory serviceScopeFactory) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.InterimStatement;
    public static CodalVersion CodalVersion => CodalVersion.V2;
    public static LetterPart LetterPart => LetterPart.NonOperationIncomeAndExpenses;

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
            "interpretativeReportSummaryPage5"
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
        RootInterpretativeReportSummaryPage5? rootInterpretativeReportSummary = jObject.ToObject<RootInterpretativeReportSummaryPage5>(
            new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        if (rootInterpretativeReportSummary?.InterpretativeReportSummaryPage5 is null)
        {
            return;
        }

        if (rootInterpretativeReportSummary.InterpretativeReportSummaryPage5.NonOperationIncomeAndExpenses is null)
        {
            return;
        }

        if (!rootInterpretativeReportSummary.InterpretativeReportSummaryPage5.NonOperationIncomeAndExpenses.IsValidReport())
        {
            return;
        }

        CodalNonOperationIncomeAndExpenses? nonOperationIncomeAndExpenses =
            rootInterpretativeReportSummary.InterpretativeReportSummaryPage5.NonOperationIncomeAndExpenses;

        nonOperationIncomeAndExpenses.AddCustomRowItems();

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        foreach (YearDatum yearDatum in nonOperationIncomeAndExpenses.YearData)
        {
            Symbol symbol =
                await dbContext.Symbols.FirstAsync(
                    predicate: x => x.Isin == statement.Isin,
                    cancellationToken: cancellationToken);

            if (await dbContext.NonOperationIncomeAndExpenses
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

            foreach (RowItem rowItem in nonOperationIncomeAndExpenses.RowItems.Where(x => !string.IsNullOrWhiteSpace(x.GetDescription())))
            {
                NonOperationIncomeAndExpense incomeStatement = new(
                    Guid.NewGuid(),
                    symbol,
                    statement.TracingNo,
                    statement.HtmlUrl,
                    fiscalYear: yearDatum.FiscalYear!,
                    yearEndMonth: yearDatum.FiscalMonth!.Value,
                    reportMonth: yearDatum.ReportMonth!.Value,
                    rowItem.GetDescription(),
                    rowItem.GetValue(yearDatum.ColumnId),
                    yearDatum.IsAudited,
                    yearDatum.ColumnId == ColumnId.CurrentPeriod,
                    yearDatum.ColumnId == ColumnId.LastAnnualPeriod,
                    yearDatum.ColumnId == ColumnId.PredictedPeriod,
                    DateTime.Now
                );
                dbContext.NonOperationIncomeAndExpenses.Add(incomeStatement);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}