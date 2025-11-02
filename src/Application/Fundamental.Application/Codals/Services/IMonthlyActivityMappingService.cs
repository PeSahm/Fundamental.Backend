using Fundamental.Application.Codals.Dto.MonthlyActivities.V5;
using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Services;

/// <summary>
/// Interface for Monthly Activity mapping services.
/// </summary>
public interface IMonthlyActivityMappingService : ICanonicalMappingService<CanonicalMonthlyActivity, CodalMonthlyActivityV5>
{
    // Monthly Activity specific methods can be added here if needed
}