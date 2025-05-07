using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Queries.GetSectors;

[HandlerCode(HandlerCode.GetSectors)]
public enum GetSectorsErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    SectorNotFound = 11_999_201
}