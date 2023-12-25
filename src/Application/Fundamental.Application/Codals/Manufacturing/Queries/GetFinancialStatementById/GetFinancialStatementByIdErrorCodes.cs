using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatementById;

[HandlerCode(HandlerCode.GetFinancialStatementById)]
public enum GetFinancialStatementByIdErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    FinancialStatementNotFound = 13_395_101,
}