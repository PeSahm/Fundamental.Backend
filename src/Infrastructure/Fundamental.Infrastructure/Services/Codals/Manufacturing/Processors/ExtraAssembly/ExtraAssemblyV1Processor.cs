using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.ExtraAssembly.V1;
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

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAssembly;

/// <summary>
/// Processor for Extraordinary General Meeting Decisions V1 JSON data.
/// </summary>
public sealed class ExtraAssemblyV1Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.ExtraordinaryGeneralMeetingDecisions;
    public static CodalVersion CodalVersion => CodalVersion.V1;
    public static LetterPart LetterPart => LetterPart.NotSpecified;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        Symbol? symbol = await dbContext.Symbols.FirstOrDefaultAsync(s => s.Isin == statement.Isin, cancellationToken);
        if (symbol == null)
        {
            throw new InvalidOperationException($"Symbol with ISIN {statement.Isin} not found");
        }

        // Deserialize JSON directly to DTO
        CodalExtraAssemblyV1? dto = JsonConvert.DeserializeObject<CodalExtraAssemblyV1>(model.Json);
        if (dto == null)
        {
            throw new InvalidOperationException("Failed to deserialize ExtraAssembly data");
        }

        if (!dto.IsValidReport())
        {
            throw new InvalidOperationException("Invalid ExtraAssembly report: missing parent assembly data");
        }

        // Get mapping service
        ICanonicalMappingService<CanonicalExtraAssembly, CodalExtraAssemblyV1> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalExtraAssembly, CodalExtraAssemblyV1>();

        // Map to canonical
        CanonicalExtraAssembly canonical = await mappingService.MapToCanonicalAsync(dto, symbol, statement);

        // Upsert
        CanonicalExtraAssembly? existing = await dbContext.CanonicalExtraAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == statement.TracingNo, cancellationToken);

        if (existing != null)
        {
            mappingService.UpdateCanonical(existing, canonical);
        }
        else
        {
            await dbContext.CanonicalExtraAssemblies.AddAsync(canonical, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
