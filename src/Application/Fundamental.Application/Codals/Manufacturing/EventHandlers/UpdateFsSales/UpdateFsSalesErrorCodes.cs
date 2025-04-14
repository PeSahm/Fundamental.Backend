using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsSales;

[HandlerCode(HandlerCode.UpdateFinancialStatementSales)]
public enum UpdateFsSalesErrorCodes
{
    [ErrorType(BackendErrorType.ApplicationFailure)]
    StatementNotFound = 13_377_101
}