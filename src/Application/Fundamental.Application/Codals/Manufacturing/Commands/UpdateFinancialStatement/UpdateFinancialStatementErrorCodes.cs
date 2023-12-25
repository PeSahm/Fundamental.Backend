using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateFinancialStatement;

[HandlerCode(HandlerCode.UpdateFinancialStatement)]
public enum UpdateFinancialStatementErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    SymbolNotFound = 13_393_101,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateTraceNo = 13_393_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateStatement = 13_393_103,

    [ErrorType(BackendErrorType.Security)]
    StatementNotFound = 13_393_104,
}