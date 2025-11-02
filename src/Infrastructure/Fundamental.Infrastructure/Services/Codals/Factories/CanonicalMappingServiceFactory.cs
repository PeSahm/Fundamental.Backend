using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Common.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Services.Codals.Factories;

/// <summary>
/// Factory for resolving canonical mapping services based on DTO metadata.
/// </summary>
public class CanonicalMappingServiceFactory : ICanonicalMappingServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="CanonicalMappingServiceFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider for resolving keyed services.</param>
    public CanonicalMappingServiceFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Gets the appropriate mapping service for the specified canonical and DTO types.
    /// </summary>
    /// <typeparam name="TCanonical">The canonical entity type.</typeparam>
    /// <typeparam name="TDto">The DTO type that implements ICodalMappingServiceMetadata.</typeparam>
    /// <returns>The mapping service instance.</returns>
    public ICanonicalMappingService<TCanonical, TDto> GetMappingService<TCanonical, TDto>()
        where TCanonical : BaseEntity<Guid>
        where TDto : class, ICodalMappingServiceMetadata
    {
        if (_serviceProvider is not IKeyedServiceProvider keyedServiceProvider)
        {
            throw new InvalidOperationException("Keyed Services Not Supported");
        }

        // Get metadata from the DTO type using reflection
        ICodalMappingServiceMetadata metadata = GetMetadataFromDtoType(typeof(TDto));
        string key = MappingServiceKey(metadata.ReportingType, metadata.LetterType, metadata.CodalVersion, metadata.LetterPart);

        return (ICanonicalMappingService<TCanonical, TDto>)keyedServiceProvider.GetRequiredKeyedService(
            typeof(ICanonicalMappingService<TCanonical, TDto>),
            key);
    }

    /// <summary>
    /// Retrieves metadata from a DTO type by creating an instance.
    /// </summary>
    /// <param name="dtoType">The DTO type.</param>
    /// <returns>The metadata instance.</returns>
    private static ICodalMappingServiceMetadata GetMetadataFromDtoType(Type dtoType)
    {
        // Create a dummy instance to access metadata properties
        // In practice, you might want to cache this or use a different approach
        if (Activator.CreateInstance(dtoType) is not ICodalMappingServiceMetadata instance)
        {
            throw new InvalidOperationException($"Cannot create metadata instance for DTO type {dtoType.Name}");
        }

        return instance;
    }

    /// <summary>
    /// Generates a service key from the metadata components.
    /// </summary>
    /// <param name="reportingType">The reporting type.</param>
    /// <param name="letterType">The letter type.</param>
    /// <param name="version">The CODAL version.</param>
    /// <param name="letterPart">The letter part.</param>
    /// <returns>The service key string.</returns>
    private static string MappingServiceKey(ReportingType reportingType, LetterType letterType, CodalVersion version, LetterPart letterPart)
    {
        return $"{reportingType}-{letterType}-{version}-{letterPart}";
    }
}