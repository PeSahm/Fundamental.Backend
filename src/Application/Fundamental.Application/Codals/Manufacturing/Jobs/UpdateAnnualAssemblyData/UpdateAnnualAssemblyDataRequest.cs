using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateAnnualAssemblyData;

[HandlerCode(HandlerCode.UpdateAnnualAssemblyData)]
public sealed record UpdateAnnualAssemblyDataRequest(uint DaysBefore) : IRequest<Response>;
