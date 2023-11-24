using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Commands.AddSymbolRelation;

[HandlerCode(HandlerCode.AddSymbolRelationship)]
public enum AddSymbolRelationRequestErrorCodes
{
    [ErrorType(BackendErrorType.Security)]
    InvestorIsInvalid = 13_399_101,

    [ErrorType(BackendErrorType.Security)]
    InvestmentIsInvalid = 13_399_102,

    [ErrorType(BackendErrorType.BusinessLogic)]
    RatioIsInvalid = 13_399_103,

    [ErrorType(BackendErrorType.BusinessLogic)]
    RelationAlreadyExists = 13_399_104,
}