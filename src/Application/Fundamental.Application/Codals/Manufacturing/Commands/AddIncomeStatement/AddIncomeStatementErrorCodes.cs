using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddIncomeStatement;

[HandlerCode(HandlerCode.AddIncomeStatement)]
public enum AddIncomeStatementErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    SymbolNotFound = 13_386_101,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateTraceNo = 13_386_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    DuplicateStatement = 13_386_103,

    [ErrorType(BackendErrorType.BusinessLogic)]
    SomeCodalRowsAreInvalid = 13_386_104
}