using Fundamental.Application.Symbols.Queries.GetSymbolRelations;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelationById;

[HandlerCode(HandlerCode.GetSymbolRelationById)]
public record GetSymbolRelationByIdRequest(Guid Id) : IRequest<Response<GetSymbolRelationsResultItem>>;