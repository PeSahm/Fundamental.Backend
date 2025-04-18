using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;

public sealed class
    GetMonthlyActivityByIdQueryHandler(IRepository repository)
    : IRequestHandler<GetMonthlyActivityByIdRequest, Response<GetMonthlyActivitiesResultItem>>
{
    public async Task<Response<GetMonthlyActivitiesResultItem>> Handle(
        GetMonthlyActivityByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetMonthlyActivitiesResultItem? result = await repository.FirstOrDefaultAsync(
            new MonthlyActivitySpec()
                .WhereId(request.Id)
                .Select(),
            cancellationToken
        );

        if (result is null)
        {
            return GetMonthlyActivityByIdErrorCodes.MonthlyActivityNotFound;
        }

        return result;
    }
}