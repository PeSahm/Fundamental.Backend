using Fundamental.Application.Codals.Dto.ExtraAnnualAssembly.V1;
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

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.ExtraAnnualAssembly;

/// <summary>
/// Processor for Extraordinary Annual Assembly V1 JSON data.
/// Uses dedicated CanonicalExtraAnnualAssembly entity for storage.
/// </summary>
public sealed class ExtraAnnualAssemblyV1Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.OrdinaryGeneralMeetingExtraordinaryDecisions;
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
        CodalExtraAnnualAssemblyV1? dto = JsonConvert.DeserializeObject<CodalExtraAnnualAssemblyV1>(model.Json);
        if (dto == null)
        {
            throw new InvalidOperationException("Failed to deserialize Extraordinary Annual Assembly data");
        }

        // Get mapping service
        ICanonicalMappingService<CanonicalExtraAnnualAssembly, CodalExtraAnnualAssemblyV1> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalExtraAnnualAssembly, CodalExtraAnnualAssemblyV1>();

        // Map to canonical
        CanonicalExtraAnnualAssembly canonical = await mappingService.MapToCanonicalAsync(dto, symbol, statement);

        // Upsert
        CanonicalExtraAnnualAssembly? existing = await dbContext.CanonicalExtraAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == statement.TracingNo, cancellationToken);

        if (existing != null)
        {
            mappingService.UpdateCanonical(existing, canonical);
        }
        else
        {
            await dbContext.CanonicalExtraAnnualAssemblies.AddAsync(canonical, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
