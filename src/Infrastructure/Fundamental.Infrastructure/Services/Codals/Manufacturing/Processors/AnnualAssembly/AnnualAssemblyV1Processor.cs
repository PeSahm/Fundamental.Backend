using Fundamental.Application.Codals.Dto.AnnualAssembly.V1;
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

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.AnnualAssembly;

/// <summary>
/// Processor for Annual Assembly V1 JSON data.
/// </summary>
public sealed class AnnualAssemblyV1Processor(
    IServiceScopeFactory serviceScopeFactory,
    ICanonicalMappingServiceFactory mappingServiceFactory
) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.AnnualGeneralMeetingDecisions;
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
        CodalAnnualAssemblyV1? dto = JsonConvert.DeserializeObject<CodalAnnualAssemblyV1>(model.Json);
        if (dto == null)
        {
            throw new InvalidOperationException("Failed to deserialize Annual Assembly data");
        }

        // Get mapping service
        ICanonicalMappingService<CanonicalAnnualAssembly, CodalAnnualAssemblyV1> mappingService =
            mappingServiceFactory.GetMappingService<CanonicalAnnualAssembly, CodalAnnualAssemblyV1>();

        // Map to canonical
        CanonicalAnnualAssembly canonical = await mappingService.MapToCanonicalAsync(dto, symbol, statement);

        // Upsert
        CanonicalAnnualAssembly? existing = await dbContext.CanonicalAnnualAssemblies
            .FirstOrDefaultAsync(x => x.Symbol.Id == symbol.Id && x.TraceNo == statement.TracingNo, cancellationToken);

        if (existing != null)
        {
            mappingService.UpdateCanonical(existing, canonical);
        }
        else
        {
            await dbContext.CanonicalAnnualAssemblies.AddAsync(canonical, cancellationToken);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
