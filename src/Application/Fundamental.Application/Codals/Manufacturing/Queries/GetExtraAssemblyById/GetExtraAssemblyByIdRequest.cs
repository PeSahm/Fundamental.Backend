using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;

[HandlerCode(HandlerCode.GetExtraAssemblyById)]
public sealed record GetExtraAssemblyByIdRequest(
    Guid Id
) : IRequest<Response<GetExtraAssemblyDetailItem>>;
