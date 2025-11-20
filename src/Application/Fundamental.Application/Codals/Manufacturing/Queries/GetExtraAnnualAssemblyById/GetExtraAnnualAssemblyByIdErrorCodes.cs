using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblyById;

[HandlerCode(HandlerCode.GetExtraAnnualAssemblyById)]
public enum GetExtraAnnualAssemblyByIdErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    ExtraAnnualAssemblyNotFound = 13_375_101
}
