using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Commands.ApproveSymbolShareHolder;

[HandlerCode(HandlerCode.ApproveSymbolShareHolder)]
public enum ApproveSymbolShareHolderErrorCodes
{
    IdIsNotValid = 11_394_101,
    ShareHolderIsinIsNotValid = 11_394_102,
}