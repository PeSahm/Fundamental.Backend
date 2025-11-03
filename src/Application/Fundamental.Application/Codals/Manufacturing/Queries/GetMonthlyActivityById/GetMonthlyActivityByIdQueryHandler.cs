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
    /// <summary>
    /// Retrieves a monthly activity by its identifier and returns the corresponding detail item or an error response.
    /// </summary>
    /// <param name="request">The request containing the identifier of the monthly activity to retrieve.</param>
    /// <param name="cancellationToken">Token to observe while waiting for the operation to complete.</param>
    /// <returns>A <see cref="Response{GetMonthlyActivityDetailItem}"/> containing the requested detail item if found, or a response with the <c>MonthlyActivityNotFound</c> error code if no matching activity exists.</returns>
    public async Task<Response<GetMonthlyActivityDetailItem>> Handle(
        GetMonthlyActivityByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetMonthlyActivityDetailItem? result = await repository.FirstOrDefaultAsync(
            new MonthlyActivityResultItemSpec()
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