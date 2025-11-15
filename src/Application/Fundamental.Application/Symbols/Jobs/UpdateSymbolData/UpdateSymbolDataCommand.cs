using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Jobs.UpdateSymbolData;

[HandlerCode(HandlerCode.UpdateSymbolData)]
public sealed record UpdateSymbolDataCommand : IRequest<Response>;