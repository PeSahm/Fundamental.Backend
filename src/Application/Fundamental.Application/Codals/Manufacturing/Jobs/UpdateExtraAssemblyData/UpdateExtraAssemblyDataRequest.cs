using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateExtraAssemblyData;

[HandlerCode(HandlerCode.UpdateExtraAssemblyData)]
public sealed record UpdateExtraAssemblyDataRequest(uint DaysBefore) : IRequest<Response>;
