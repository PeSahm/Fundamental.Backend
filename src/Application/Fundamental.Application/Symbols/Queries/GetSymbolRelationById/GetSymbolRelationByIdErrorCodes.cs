using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelationById;

[HandlerCode(HandlerCode.GetSymbolRelationById)]
public enum GetSymbolRelationByIdErrorCodes
{
    SymbolRelationNotFound = 11_307_101,
}