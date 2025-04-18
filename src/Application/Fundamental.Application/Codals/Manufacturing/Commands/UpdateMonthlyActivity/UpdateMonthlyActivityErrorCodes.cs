using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateMonthlyActivity;

[HandlerCode(HandlerCode.UpdateMonthlyActivity)]
public enum UpdateMonthlyActivityErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    SymbolNotFound = 13_392_101,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateTraceNo = 13_392_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateStatement = 13_392_103,

    [ErrorType(BackendErrorType.Security)]
    MonthlyActivityNotFound = 13_392_104
}