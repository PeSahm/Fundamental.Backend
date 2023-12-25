using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetMonthlyActivityById;

[HandlerCode(HandlerCode.GetMonthlyActivityById)]
public enum GetMonthlyActivityByIdErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    MonthlyActivityNotFound = 13_394_101,
}