using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;

[HandlerCode(HandlerCode.AddBalanceSheet)]
public enum AddBalanceSheetErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    SymbolNotFound = 13_391_101,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateTraceNo = 13_391_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateStatement = 13_391_103,

    [ErrorType(BackendErrorType.BusinessLogic)]
    SomeCodalRowsAreInvalid = 13_391_104,
}