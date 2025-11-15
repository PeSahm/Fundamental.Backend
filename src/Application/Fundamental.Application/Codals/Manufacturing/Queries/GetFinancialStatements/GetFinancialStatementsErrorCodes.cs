using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;

[HandlerCode(HandlerCode.GetFinancialStatementsData)]
public enum GetFinancialStatementsErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    RecodeNotFound = 13_378_101
}