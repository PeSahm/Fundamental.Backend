using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ForceUpdateStatements;

[HandlerCode(HandlerCode.ForceUpdateStatements)]
public enum ForceUpdateStatementErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    InvalidStatement = 13_383_101
}