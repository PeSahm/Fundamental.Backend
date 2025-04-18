using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddMonthlyActivity;

[HandlerCode(HandlerCode.AddMonthlyActivity)]
public enum AddMonthlyActivityErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    SymbolNotFound = 13_398_101,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateTraceNo = 13_398_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateStatement = 13_398_103
}