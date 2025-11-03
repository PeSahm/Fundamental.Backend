using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;

public sealed class
    GetMonthlyActivitiesQueryHandler(IMonthlyActivityRepository monthlyActivityRepository)
    : IRequestHandler<GetMonthlyActivitiesRequest, Response<Paginated<GetMonthlyActivitiesListItem>>>
{
    public async Task<Response<Paginated<GetMonthlyActivitiesListItem>>> Handle(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        return await monthlyActivityRepository.GetMonthlyActivitiesAsync(request, cancellationToken);
    }
}