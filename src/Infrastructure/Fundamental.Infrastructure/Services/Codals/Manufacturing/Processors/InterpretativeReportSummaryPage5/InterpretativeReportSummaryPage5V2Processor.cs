using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.InterpretativeReportSummaryPage5;
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

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPage5;

/// <summary>
/// Processor for Interpretative Report Summary Page 5 V2 data.
/// Handles deserialization, mapping, and persistence of all 14 sections.
/// </summary>
public sealed class InterpretativeReportSummaryPage5V2Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.InterimStatement;
    public static CodalVersion CodalVersion => CodalVersion.V2;
    public static LetterPart LetterPart => LetterPart.InterpretativeReportSummaryPage5;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JObject jObject = JObject.Parse(model.Json);

        // Filter to keep only required properties
        HashSet<string> propertiesToKeep =
        [
            "listedCapital",
            "unauthorizedCapital",
            "interpretativeReportSummaryPage5"
        ];

        List<JProperty> properties = jObject.Properties().ToList();

#pragma warning disable S3267
        foreach (JProperty property in properties)
        {
            if (!propertiesToKeep.Contains(property.Name))
            {
                property.Remove();
            }
        }
#pragma warning restore S3267

        // Deserialize filtered JSON
        RootInterpretativeReportSummaryPage5V2? root = jObject.ToObject<RootInterpretativeReportSummaryPage5V2>(
            new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        if (root?.InterpretativeReportSummaryPage5 is null)
        {
            return;
        }

        CodalInterpretativeReportSummaryPage5V2 dto = root.InterpretativeReportSummaryPage5;

        if (!dto.IsValidReport())
        {
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        Symbol symbol = await dbContext.Symbols.FirstAsync(
            x => x.Isin == statement.Isin,
            cancellationToken);

        // Get mapping service
        ICanonicalMappingService<CanonicalInterpretativeReportSummaryPage5, CodalInterpretativeReportSummaryPage5V2> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalInterpretativeReportSummaryPage5, CodalInterpretativeReportSummaryPage5V2>();

        // Map DTO to canonical entity
        CanonicalInterpretativeReportSummaryPage5 canonical = await mappingService.MapToCanonicalAsync(dto, symbol, statement);

        // Check if entity already exists
        CanonicalInterpretativeReportSummaryPage5? existing = await dbContext.CanonicalInterpretativeReportSummaryPage5s
            .FirstOrDefaultAsync(
                x =>
                    x.Symbol.Isin == statement.Isin &&
                    x.FiscalYear.Year == canonical.FiscalYear.Year &&
                    x.ReportMonth.Month == canonical.ReportMonth.Month &&
                    x.TraceNo == statement.TracingNo,
                cancellationToken);

        if (existing != null)
        {
            // Update existing entity
            mappingService.UpdateCanonical(existing, canonical);
        }
        else
        {
            // Add new entity
            dbContext.CanonicalInterpretativeReportSummaryPage5s.Add(canonical);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
