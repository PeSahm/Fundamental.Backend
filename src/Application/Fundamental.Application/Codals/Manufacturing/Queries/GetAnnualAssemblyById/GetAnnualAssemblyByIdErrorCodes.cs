using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblyById;

[HandlerCode(HandlerCode.GetAnnualAssemblyById)]
public enum GetAnnualAssemblyByIdErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    AnnualAssemblyNotFound = 13_374_101
}
