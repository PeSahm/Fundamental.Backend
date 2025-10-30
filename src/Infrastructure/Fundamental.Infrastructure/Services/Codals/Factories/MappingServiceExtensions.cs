using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.Enums;
using Microsoft.Extensions.DependencyInjection;

namespace Fundamental.Infrastructure.Services.Codals.Factories;

/// <summary>
/// Extension methods for registering canonical mapping services.
/// </summary>
public static class MappingServiceExtensions
{
    /// <summary>
    /// Adds the canonical mapping service factory to the service collection.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddCanonicalMappingServiceFactory(this IServiceCollection services)
    {
        return services.AddSingleton<ICanonicalMappingServiceFactory, CanonicalMappingServiceFactory>();
    }

    /// <summary>
    /// Adds a keyed scoped canonical mapping service to the service collection.
    /// </summary>
    /// <typeparam name="TService">The service interface type.</typeparam>
    /// <typeparam name="TImplementation">The service implementation type.</typeparam>
    /// <typeparam name="TDto">The DTO type that implements ICodalMappingServiceMetadata.</typeparam>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection for chaining.</returns>
    public static IServiceCollection AddKeyedScopedCanonicalMappingService<TService, TImplementation, TDto>(
        this IServiceCollection services
    )
        where TService : class
        where TImplementation : class, TService
        where TDto : class, ICodalMappingServiceMetadata
    {
        return services.AddKeyedScoped(
            typeof(TService),
            MappingServiceKeyFromDto<TDto>(),
            typeof(TImplementation));
    }

    /// <summary>
    /// Gets a required keyed service using the specified metadata components.
    /// </summary>
    /// <typeparam name="T">The service type.</typeparam>
    /// <param name="provider">The service provider.</param>
    /// <param name="reportingType">The reporting type.</param>
    /// <param name="letterType">The letter type.</param>
    /// <param name="version">The CODAL version.</param>
    /// <param name="letterPart">The letter part.</param>
    /// <returns>The service instance.</returns>
    public static T GetRequiredKeyedService<T>(
        this IServiceProvider provider,
        ReportingType reportingType,
        LetterType letterType,
        CodalVersion version,
        LetterPart letterPart
    )
        where T : class
    {
        if (provider is not IKeyedServiceProvider keyedServiceProvider)
        {
            throw new InvalidOperationException("Keyed Services Not Supported");
        }

        string key = MappingServiceKey(reportingType, letterType, version, letterPart);
        return (T)keyedServiceProvider.GetRequiredKeyedService(typeof(T), key);
    }

    /// <summary>
    /// Generates a service key from the DTO type's metadata.
    /// </summary>
    /// <typeparam name="TDto">The DTO type.</typeparam>
    /// <returns>The service key string.</returns>
    private static string MappingServiceKeyFromDto<TDto>()
        where TDto : class, ICodalMappingServiceMetadata
    {
        ICodalMappingServiceMetadata metadata = GetMetadataFromDtoType(typeof(TDto));
        return MappingServiceKey(metadata.ReportingType, metadata.LetterType, metadata.CodalVersion, metadata.LetterPart);
    }

    /// <summary>
    /// Retrieves metadata from a DTO type by creating an instance.
    /// </summary>
    /// <param name="dtoType">The DTO type.</param>
    /// <returns>The metadata instance.</returns>
    private static ICodalMappingServiceMetadata GetMetadataFromDtoType(Type dtoType)
    {
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