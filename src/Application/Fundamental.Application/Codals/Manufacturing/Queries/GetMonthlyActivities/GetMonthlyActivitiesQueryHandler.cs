using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;

public sealed class
    GetMonthlyActivitiesQueryHandler(IMonthlyActivityRepository monthlyActivityRepository)
    : IRequestHandler<GetMonthlyActivitiesRequest, Response<Paginated<GetMonthlyActivitiesResultItem>>>
{
    public async Task<Response<Paginated<GetMonthlyActivitiesResultItem>>> Handle(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        return await monthlyActivityRepository.GetMonthlyActivitiesAsync(request, cancellationToken);
    }
}