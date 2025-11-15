using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Fairs.Commands.SaveFairs;

[HandlerCode(HandlerCode.SaveFair)]
public sealed record SaveFairRequest(string Json) : IRequest<Response>;