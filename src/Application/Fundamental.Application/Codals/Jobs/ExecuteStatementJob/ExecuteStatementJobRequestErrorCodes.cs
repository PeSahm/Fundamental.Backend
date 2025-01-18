using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Jobs.ExecuteStatementJob;

[HandlerCode(HandlerCode.ExecuteStatementJobRequest)]
public enum ExecuteStatementJobRequestErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    StatementNotFound = 13_193_101
}