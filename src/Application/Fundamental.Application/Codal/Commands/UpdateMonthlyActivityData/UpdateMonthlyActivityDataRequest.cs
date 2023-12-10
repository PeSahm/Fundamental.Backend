using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codal.Commands.UpdateMonthlyActivityData;

[HandlerCode(HandlerCode.UpdateMonthlyActivityData)]
public sealed record UpdateMonthlyActivityDataRequest : IRequest<Response>;