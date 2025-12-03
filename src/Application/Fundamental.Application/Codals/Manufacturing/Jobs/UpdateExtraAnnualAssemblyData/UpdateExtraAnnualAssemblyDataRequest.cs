using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateExtraAnnualAssemblyData;

[HandlerCode(HandlerCode.UpdateExtraAnnualAssemblyData)]
public sealed record UpdateExtraAnnualAssemblyDataRequest(uint DaysBefore) : IRequest<Response>;
