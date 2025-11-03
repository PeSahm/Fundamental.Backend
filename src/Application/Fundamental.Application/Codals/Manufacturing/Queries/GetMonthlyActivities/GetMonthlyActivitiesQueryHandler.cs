using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;

public sealed class
    GetMonthlyActivitiesQueryHandler(IMonthlyActivityRepository monthlyActivityRepository)
    : IRequestHandler<GetMonthlyActivitiesRequest, Response<Paginated<GetMonthlyActivitiesListItem>>>
{
    /// <summary>
    /// Retrieve paginated monthly activities for the given request.
    /// </summary>
    /// <param name="request">Query parameters and pagination options for fetching monthly activities.</param>
    /// <returns>A Response containing a paginated set of GetMonthlyActivitiesListItem that match the request.</returns>
    public async Task<Response<Paginated<GetMonthlyActivitiesListItem>>> Handle(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        return await monthlyActivityRepository.GetMonthlyActivitiesAsync(request, cancellationToken);
    }
}