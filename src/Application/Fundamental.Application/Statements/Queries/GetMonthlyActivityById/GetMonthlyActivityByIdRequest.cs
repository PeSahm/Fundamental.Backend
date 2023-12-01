using Fundamental.Application.Statements.Queries.GetMonthlyActivities;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Statements.Queries.GetMonthlyActivityById;

[HandlerCode(HandlerCode.GetMonthlyActivityById)]
public sealed record GetMonthlyActivityByIdRequest(Guid Id)
    : IRequest<Response<GetMonthlyActivitiesResultItem>>;