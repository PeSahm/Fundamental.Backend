using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;

[HandlerCode(HandlerCode.GetExtraAssemblyById)]
public enum GetExtraAssemblyByIdErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    ExtraAssemblyNotFound = 13_369_101,
}