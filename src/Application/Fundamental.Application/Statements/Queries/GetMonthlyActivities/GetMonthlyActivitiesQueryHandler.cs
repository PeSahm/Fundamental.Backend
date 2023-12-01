using Fundamental.Application.Statements.Repositories;
using Fundamental.Domain.Common.Dto;
using MediatR;

namespace Fundamental.Application.Statements.Queries.GetMonthlyActivities;

public sealed class
    GetMonthlyActivitiesQueryHandler : IRequestHandler<GetMonthlyActivitiesRequest, Paginated<GetMonthlyActivitiesResultItem>>
{
    private readonly IMonthlyActivityRepository _monthlyActivityRepository;

    public GetMonthlyActivitiesQueryHandler(IMonthlyActivityRepository monthlyActivityRepository)
    {
        _monthlyActivityRepository = monthlyActivityRepository;
    }

    public async Task<Paginated<GetMonthlyActivitiesResultItem>> Handle(
        GetMonthlyActivitiesRequest request,
        CancellationToken cancellationToken
    )
    {
        return await _monthlyActivityRepository.GetMonthlyActivitiesAsync(request, cancellationToken);
    }
}