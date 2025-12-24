using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
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

public class MonthlyActivityV5Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
)
    : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;

    public static CodalVersion CodalVersion => CodalVersion.V5;

    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;

        // The codal service has an unexpected response;  If the model.json in the Energy object has value_319524, it's ok else we should add this logic=> value_319521 = value_319522 and value_319522 = value_319523
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

        CodalMonthlyActivityV5? monthlyActivity = jsonObject.ToObject<CodalMonthlyActivityV5>(JsonSerializer.Create(setting));

        if (monthlyActivity is null)
        {
            Log.Warning("Failed to deserialize MonthlyActivity V5 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        if (monthlyActivity.MonthlyActivity is null)
        {
            Log.Warning("MonthlyActivity is null in V5 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        // Validation is delegated to mapping service; ensure YearData exists minimally.
        if (monthlyActivity.MonthlyActivity.ProductionAndSales?.YearData is null ||
            monthlyActivity.MonthlyActivity.ProductionAndSales.YearData.Count == 0)
        {
            Log.Warning("No year data found in V5 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        Symbol symbol = await dbContext.Symbols.FirstAsync(
            x => x.Isin == statement.Isin,
            cancellationToken);

        // Get the mapping service for V5
        ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>();

        // Map to canonical entity
        CanonicalMonthlyActivity canonical = await mappingService.MapToCanonicalAsync(monthlyActivity, symbol, statement);
        canonical.PublishDate = statement.PublishDateMiladi.ToUniversalTime();

        // Check for existing canonical record
        CanonicalMonthlyActivity? existingCanonical = await dbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(
                x => x.Symbol.Isin == statement.Isin &&
                     x.FiscalYear.Year == canonical.FiscalYear.Year &&
                     x.ReportMonth.Month == canonical.ReportMonth.Month,
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
            "Successfully processed MonthlyActivity V5 for Symbol: {Isin}, FiscalYear: {FiscalYear}, ReportMonth: {ReportMonth}",
            statement.Isin,
            canonical.FiscalYear.Year,
            canonical.ReportMonth.Month);
    }
}