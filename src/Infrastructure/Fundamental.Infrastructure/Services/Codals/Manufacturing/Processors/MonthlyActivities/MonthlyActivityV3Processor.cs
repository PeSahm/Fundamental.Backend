using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V3;
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

public class MonthlyActivityV3Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
)
    : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;

    public static CodalVersion CodalVersion => CodalVersion.V3;

    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;
        CodalMonthlyActivityV3? monthlyActivity =
            JsonConvert.DeserializeObject<CodalMonthlyActivityV3>(model.Json, setting);

        if (monthlyActivity is null)
        {
            Log.Warning("Failed to deserialize MonthlyActivity V3 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        if (monthlyActivity.MonthlyActivity is null)
        {
            Log.Warning("MonthlyActivity is null in V3 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        Symbol symbol = await dbContext.Symbols.FirstAsync(
            x => x.Isin == statement.Isin,
            cancellationToken);

        // Check for existing record
        RawMonthlyActivityJson? existingRawJson = await dbContext.RawMonthlyActivityJsons
            .FirstOrDefaultAsync(
                x => x.Symbol.Id == symbol.Id &&
                     x.Version == CodalVersion.V3,
                cancellationToken);

        // Store raw JSON
        if (existingRawJson == null)
        {
            RawMonthlyActivityJson rawJson = new()
            {
                TraceNo = (long)statement.TracingNo,
                Symbol = symbol,
                PublishDate = statement.PublishDateMiladi,
                Version = CodalVersion.V3,
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
                existingRawJson.RawJson = model.Json;
            }
        }

        // Get the mapping service for V3
        ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV3> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV3>();

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
                UpdateCanonicalMonthlyActivity(existingCanonical, canonical);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
        Log.Information(
            "Successfully processed MonthlyActivity V3 for Symbol: {Isin}, FiscalYear: {FiscalYear}, ReportMonth: {ReportMonth}",
            statement.Isin,
            canonical.FiscalYear,
            canonical.ReportMonth);
    }

    private static void UpdateCanonicalMonthlyActivity(CanonicalMonthlyActivity existing, CanonicalMonthlyActivity updated)
    {
        existing.TraceNo = updated.TraceNo;
        existing.Uri = updated.Uri;
        existing.Currency = updated.Currency;
        existing.HasSubCompanySale = updated.HasSubCompanySale;

        // Update collections
        existing.ProductionAndSalesItems = updated.ProductionAndSalesItems;
        existing.Descriptions = updated.Descriptions;
    }
}