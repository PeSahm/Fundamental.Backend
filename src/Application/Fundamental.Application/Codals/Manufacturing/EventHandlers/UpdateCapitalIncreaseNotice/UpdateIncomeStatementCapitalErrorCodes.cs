using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateCapitalIncreaseNotice;

[HandlerCode(HandlerCode.UpdateCapitalIncreaseRegistrationNotice)]
public enum UpdateIncomeStatemetnCapitalErrorCodes
{
    [ErrorType(BackendErrorType.ApplicationFailure)]
    NoticeNotFound = 13_394_101
}