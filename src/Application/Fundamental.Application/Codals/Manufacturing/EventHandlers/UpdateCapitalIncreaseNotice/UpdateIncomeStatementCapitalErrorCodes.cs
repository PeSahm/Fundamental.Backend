using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateCapitalIncreaseNotice;

[HandlerCode(HandlerCode.UpdateIncomeStatementCapital)]
public enum UpdateIncomeStatementCapitalErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    CapitalRecordNotFound = 13_190_101,

    [ErrorType(BackendErrorType.Security)]
    FsRecordNotFound = 13_190_102
}