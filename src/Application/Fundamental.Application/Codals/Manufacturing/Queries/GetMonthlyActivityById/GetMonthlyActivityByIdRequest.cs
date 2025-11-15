using Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivities;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;

[HandlerCode(HandlerCode.GetMonthlyActivityById)]
public sealed record GetMonthlyActivityByIdRequest(Guid Id)
    : IRequest<Response<GetMonthlyActivityDetailItem>>;