using Fundamental.Application.Codals.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

public class MonthlyActivityV4Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
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

        // The codal service has an unexpected response;  If the model.json in the Enregy object has value_319524, it's ok else we should add this logic=> value_319521 = value_319522 and value_319522 = value_319523
        JObject jsonObject = JObject.Parse(model.Json);
        JToken? energyToken = jsonObject["monthlyActivity"]?["energy"];

        if (energyToken?["rowItems"] is JArray rowItems)
        {
            foreach (JToken row in rowItems)
            {
                // Check if value_319523 exists (the last column in the expected structure)
                JToken? value319523 = row["value_319523"];
                JToken? value319524 = row["value_319524"];

                // If value_319524 is missing, shift values left
                if (value319524 == null && value319523 != null)
                {
                    row["value_319524"] = value319523;
                    row["value_319523"] = row["value_319522"];
                    row["value_319522"] = row["value_319521"];
                    row["value_319521"] = 0;
                }
            }
        }

        CodalMonthlyActivity? monthlyActivity = jsonObject.ToObject<CodalMonthlyActivity>(JsonSerializer.Create(setting));

        if (monthlyActivity is null)
        {
            Log.Warning("Failed to deserialize MonthlyActivity V4 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        if (monthlyActivity.MonthlyActivity is null)
        {
            Log.Warning("MonthlyActivity is null in V4 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        if (monthlyActivity.MonthlyActivity.ProductionAndSales is null)
        {
            Log.Warning("ProductionAndSales is null in V4 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        if (monthlyActivity.MonthlyActivity.ProductionAndSales.YearData.Count == 0)
        {
            Log.Warning("No year data found in V4 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        YearDatum? yearDatum = monthlyActivity.MonthlyActivity.ProductionAndSales.YearData
            .Find(x => x.ColumnId == SaleColumnId.ProduceThisMonth);

        if (yearDatum is null || yearDatum.FiscalYear is null || yearDatum.ReportMonth is null)
        {
            Log.Warning("Could not extract fiscal year or report month from V4 data for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        Symbol symbol = await dbContext.Symbols.FirstAsync(
            x => x.Isin == statement.Isin,
            cancellationToken);

        // Get the mapping service for V4
        ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivity> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalMonthlyActivity, CodalMonthlyActivity>();

        // Map to canonical entity
        CanonicalMonthlyActivity canonical = await mappingService.MapToCanonicalAsync(monthlyActivity, symbol, statement);
        canonical.PublishDate = statement.PublishDateMiladi.ToUniversalTime();

        // Check for existing canonical record
        CanonicalMonthlyActivity? existingCanonical = await dbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(
                x => x.Symbol.Isin == statement.Isin &&
                     x.FiscalYear.Year == canonical.FiscalYear &&
                     x.ReportMonth.Month == canonical.ReportMonth,
                cancellationToken);

        if (existingCanonical == null)
        {
            dbContext.Add(canonical);
        }
        else
        {
            if (existingCanonical.TraceNo <= statement.TracingNo)
            {
                mappingService.UpdateCanonical(existingCanonical, canonical);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        Log.Information(
            "Successfully processed MonthlyActivity V4 for Symbol: {Isin}, FiscalYear: {FiscalYear}, ReportMonth: {ReportMonth}",
            statement.Isin,
            canonical.FiscalYear,
            canonical.ReportMonth);
    }
}