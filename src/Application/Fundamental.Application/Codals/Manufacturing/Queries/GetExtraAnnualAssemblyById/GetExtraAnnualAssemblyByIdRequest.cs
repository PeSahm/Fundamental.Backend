using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblyById;

[HandlerCode(HandlerCode.GetExtraAnnualAssemblyById)]
public sealed record GetExtraAnnualAssemblyByIdRequest(
    Guid Id
) : IRequest<Response<GetExtraAnnualAssemblyDetailItem>>;
