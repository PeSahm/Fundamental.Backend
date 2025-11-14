using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Commands.ApproveSymbolShareHolder;

[HandlerCode(HandlerCode.ApproveSymbolShareHolder)]
public enum ApproveSymbolShareHolderErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    IdIsNotValid = 11_394_101,

    [ErrorType(BackendErrorType.Security)]
    ShareHolderIsinIsNotValid = 11_394_102
}