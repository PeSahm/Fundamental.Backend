using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddFinancialStatement;

[HandlerCode(HandlerCode.AddFinancialStatement)]
public enum AddFinancialStatementErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    SymbolNotFound = 13_399_101,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateTraceNo = 13_399_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateStatement = 13_399_103,
}