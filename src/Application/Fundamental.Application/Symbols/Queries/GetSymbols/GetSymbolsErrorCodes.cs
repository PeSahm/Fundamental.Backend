using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

[HandlerCode(HandlerCode.GetSymbols)]
public enum GetSymbolsErrorCodes
{
    [ErrorType(BackendErrorType.BusinessLogic)]
    SymbolNotFound = 11_990_101,
}