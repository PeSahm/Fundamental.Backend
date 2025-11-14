using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateMonthlyActivityData;

[HandlerCode(HandlerCode.UpdateMonthlyActivityData)]
public sealed record UpdateMonthlyActivityDataRequest(int Days) : IRequest<Response>;