using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Jobs.UpdateCodalPublisherData;

[HandlerCode(HandlerCode.UpdateCodalPublisher)]
public sealed record UpdateCodalPublisherDataRequest : IRequest<Response>;

