using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsClosePrice;

[HandlerCode(HandlerCode.UpdateFinancialStatementClosePrice)]

public enum UpdateFsClosePriceErrorCodes
{
    [ErrorType(BackendErrorType.ApplicationFailure)]
    StatemtentNotFound = 13_393_101,
}