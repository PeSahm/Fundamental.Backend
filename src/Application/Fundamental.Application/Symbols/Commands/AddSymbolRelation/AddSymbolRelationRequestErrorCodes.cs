using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Commands.AddSymbolRelation;

[HandlerCode(HandlerCode.AddSymbolRelationship)]
public enum AddSymbolRelationRequestErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    InvestorIsInvalid = 11_399_101,

    [ErrorType(BackendErrorType.Security)]
    InvestmentIsInvalid = 11_399_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    RatioIsInvalid = 11_399_103,

    [ErrorType(BackendErrorType.BusinessLogic)]
    RelationAlreadyExists = 11_399_104
}