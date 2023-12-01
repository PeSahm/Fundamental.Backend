using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Statements.Repositories;

public interface IMonthlyActivityRepository
{
    Task<Paginated<GetMonthlyActivitiesResultItem>> GetMonthlyActivitiesAsync(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    );
}