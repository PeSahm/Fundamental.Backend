using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Commands.ApproveSymbolShareHolder;

[HandlerCode(HandlerCode.ApproveSymbolShareHolder)]
public sealed record ApproveSymbolShareHolderRequest(Guid Id, string ShareHolderIsin) : IRequest<Response>;