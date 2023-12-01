using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries;

[HandlerCode(HandlerCode.GetSymbolRelations)]
public sealed record GetSymbolRelationsRequest(string InvestorIsin) :
    PagingRequest, IRequest<Response<Paginated<GetSymbolRelationsResultItem>>>;