using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.RejectSymbolShareHolder;

[HandlerCode(HandlerCode.RejectSymbolShareHolder)]
public sealed record RejectSymbolShareHolderRequest(Guid Id) : IRequest<Response>;