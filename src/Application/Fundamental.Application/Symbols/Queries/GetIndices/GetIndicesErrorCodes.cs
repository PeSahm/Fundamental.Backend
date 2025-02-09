using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Queries.GetIndices;

[HandlerCode(HandlerCode.GetIndices)]
public enum GetIndicesErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    IndexNotFound = 11_998_101
}