using Fundamental.Application.Codals.Services;
using Fundamental.Domain.Common.BaseTypes;
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

        return keyedServiceProvider.GetRequiredKeyedService<TCanonical, TDto>();
    }
}