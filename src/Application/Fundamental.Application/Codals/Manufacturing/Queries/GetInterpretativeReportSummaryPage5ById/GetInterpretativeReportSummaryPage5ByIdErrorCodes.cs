using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;

[HandlerCode(HandlerCode.GetInterpretativeReportSummaryPage5ById)]
public enum GetInterpretativeReportSummaryPage5ByIdErrorCodes
{
    [ErrorType(ErrorType.NotFound)]
    InterpretativeReportSummaryPage5NotFound = 13_400_101
}
