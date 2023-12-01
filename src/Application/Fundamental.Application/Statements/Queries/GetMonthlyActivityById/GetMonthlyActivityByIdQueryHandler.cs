using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.Application.Statements.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Statements.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Statements.Queries.GetMonthlyActivityById;

public sealed class
    GetMonthlyActivityByIdQueryHandler : IRequestHandler<GetMonthlyActivityByIdRequest, Response<GetMonthlyActivitiesResultItem>>
{
    private readonly IRepository<MonthlyActivity> _repository;

    public GetMonthlyActivityByIdQueryHandler(IRepository<MonthlyActivity> repository)
    {
        _repository = repository;
    }

    public async Task<Response<GetMonthlyActivitiesResultItem>> Handle(
        GetMonthlyActivityByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetMonthlyActivitiesResultItem? result = await _repository.FirstOrDefaultAsync(
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