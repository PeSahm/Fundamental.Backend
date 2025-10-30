using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Domain.Common.BaseTypes;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Codals.Services;

/// <summary>
/// Interface for canonical mapping services that transform DTOs to canonical entities.
/// </summary>
/// <typeparam name="TCanonical">The canonical entity type.</typeparam>
/// <typeparam name="TDto">The DTO type that implements ICodalMappingServiceMetadata.</typeparam>
public interface ICanonicalMappingService<TCanonical, TDto>
    where TCanonical : BaseEntity<Guid>
    where TDto : class, ICodalMappingServiceMetadata
{
    /// <summary>
    /// Maps a DTO to a canonical entity.
    /// </summary>
    /// <param name="dto">The DTO to map from.</param>
    /// <param name="symbol">The associated symbol entity.</param>
    /// <param name="statement">The statement response data.</param>
    /// <returns>The mapped canonical entity.</returns>
    Task<TCanonical> MapToCanonicalAsync(TDto dto, Symbol symbol, GetStatementResponse statement);

    /// <summary>
    /// Updates an existing canonical entity with data from another canonical entity.
    /// </summary>
    /// <param name="existing">The existing canonical entity to update.</param>
    /// <param name="updated">The updated canonical entity data.</param>
    void UpdateCanonical(TCanonical existing, TCanonical updated);
}