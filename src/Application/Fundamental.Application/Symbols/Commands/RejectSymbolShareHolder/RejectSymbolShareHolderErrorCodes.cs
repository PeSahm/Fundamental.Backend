using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Commands.RejectSymbolShareHolder;

[HandlerCode(HandlerCode.RejectSymbolShareHolder)]
public enum RejectSymbolShareHolderErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    NotFound = 11_395_101
}