using Fundamental.Application.Codals.Dto.MonthlyActivities.V1;
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
using Serilog;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.MonthlyActivities;

public class MonthlyActivityV1Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
)
    : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;

    public static LetterType LetterType => LetterType.MonthlyActivity;

    public static CodalVersion CodalVersion => CodalVersion.V1;

    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;
        CodalMonthlyActivityV1? monthlyActivity =
            JsonConvert.DeserializeObject<CodalMonthlyActivityV1>(model.Json, setting);

        if (monthlyActivity is null)
        {
            Log.Warning("Failed to deserialize MonthlyActivity V1 JSON for TraceNo: {TraceNo}", statement.TracingNo);
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        Symbol symbol = await dbContext.Symbols.FirstAsync(
            x => x.Isin == statement.Isin,
            cancellationToken);

        // Get the mapping service for V1
        ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV1> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV1>();

        // Map to canonical entity
        CanonicalMonthlyActivity canonical = await mappingService.MapToCanonicalAsync(monthlyActivity, symbol, statement);
        canonical.PublishDate = statement.PublishDateMiladi.ToUniversalTime();

        // Extract fiscal year and report month for existing record check
        int fiscalYear = canonical.FiscalYear;
        int reportMonth = canonical.ReportMonth;
        CanonicalMonthlyActivity? existingCanonical = await dbContext.CanonicalMonthlyActivities
            .FirstOrDefaultAsync(
                x => x.Symbol.Isin == statement.Isin &&
                     x.FiscalYear.Year == fiscalYear &&
                     x.ReportMonth.Month == reportMonth,
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
            "Successfully processed MonthlyActivity V1 for Symbol: {Isin}, FiscalYear: {FiscalYear}, ReportMonth: {ReportMonth}",
            statement.Isin,
            fiscalYear,
            reportMonth);
    }
}