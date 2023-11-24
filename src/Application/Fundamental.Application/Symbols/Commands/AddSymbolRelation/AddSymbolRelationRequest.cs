using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.AddSymbolRelation;

[HandlerCode(HandlerCode.AddSymbolRelationship)]
public sealed record AddSymbolRelationRequest(string Investor, string Investment, float Ratio) : IRequest<Response>;