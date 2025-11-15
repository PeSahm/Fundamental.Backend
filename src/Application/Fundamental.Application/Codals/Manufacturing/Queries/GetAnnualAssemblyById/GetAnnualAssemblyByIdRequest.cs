using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblyById;

[HandlerCode(HandlerCode.GetAnnualAssemblyById)]
public sealed record GetAnnualAssemblyByIdRequest(
    Guid Id
) : IRequest<Response<GetAnnualAssemblyDetailItem>>;
