using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V5.Enums;
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
        CodalMonthlyActivityV5? monthlyActivity =
            JsonConvert.DeserializeObject<CodalMonthlyActivityV5>(model.Json, setting);

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

        // Extract fiscal year and report month from productionAndSales yearData
        // Find the yearData entry with the highest period (most recent month) to determine current reporting period
        YearDatumV5? yearDatum = monthlyActivity.MonthlyActivity.ProductionAndSales?.YearData
            .Find(x => x.ColumnId == ((int)ProductionAndSalesV5ColumnId.SaleThisMonth).ToString());

        if (yearDatum is null || yearDatum.FiscalYear is null || yearDatum.ReportMonth is null)
        {
            Log.Warning("Could not extract fiscal year or report month from V5 data for TraceNo: {TraceNo}", statement.TracingNo);
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

        // Store raw JSON
        RawMonthlyActivityJson? existingRawJson = await dbContext.RawMonthlyActivityJsons
            .FirstOrDefaultAsync(
                x => x.Symbol.Id == symbol.Id &&
                     x.Version == "V5",
                cancellationToken);

        if (existingRawJson == null)
        {
            RawMonthlyActivityJson rawJson = new()
            {
                TraceNo = (long)statement.TracingNo,
                Symbol = symbol,
                PublishDate = statement.PublishDateMiladi,
                Version = "5",
                RawJson = model.Json
            };
            dbContext.Add(rawJson);
        }
        else
        {
            if (existingRawJson.TraceNo <= (long)statement.TracingNo)
            {
                existingRawJson.TraceNo = (long)statement.TracingNo;
                existingRawJson.PublishDate = statement.PublishDateMiladi;
                existingRawJson.RawJson = JsonConvert.SerializeObject(monthlyActivity.MonthlyActivity, setting);
            }
        }

        // Check for existing canonical record
        CanonicalMonthlyActivity? existingCanonical = await dbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(
                x => x.Symbol.Isin == statement.Isin &&
                     x.FiscalYear.Year == yearDatum.FiscalYear &&
                     x.ReportMonth.Month == yearDatum.ReportMonth,
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
            yearDatum.FiscalYear,
            yearDatum.ReportMonth);
    }
}