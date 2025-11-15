using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;

public sealed class
    GetMonthlyActivityByIdQueryHandler(IRepository repository)
    : IRequestHandler<GetMonthlyActivityByIdRequest, Response<GetMonthlyActivityDetailItem>>
{
    public async Task<Response<GetMonthlyActivityDetailItem>> Handle(
        GetMonthlyActivityByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetMonthlyActivityDetailItem? result = await repository.FirstOrDefaultAsync(
            new MonthlyActivityDetailItemSpec()
                .WhereId(request.Id),
            cancellationToken
        );

        if (result is null)
        {
            return GetMonthlyActivityByIdErrorCodes.MonthlyActivityNotFound;
        }

        return result;
    }
}