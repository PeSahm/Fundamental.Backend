using Fundamental.Domain.Common.BaseTypes;

namespace Fundamental.Application.Codals.Services;

/// <summary>
/// Factory interface for resolving canonical mapping services based on DTO types.
/// </summary>
public interface ICanonicalMappingServiceFactory
{
    /// <summary>
    /// Gets the appropriate mapping service for the specified canonical and DTO types.
    /// </summary>
    /// <typeparam name="TCanonical">The canonical entity type.</typeparam>
    /// <typeparam name="TDto">The DTO type that implements ICodalMappingServiceMetadata.</typeparam>
    /// <returns>The mapping service instance.</returns>
    ICanonicalMappingService<TCanonical, TDto> GetMappingService<TCanonical, TDto>()
        where TCanonical : BaseEntity<Guid>
        where TDto : class, ICodalMappingServiceMetadata;
}