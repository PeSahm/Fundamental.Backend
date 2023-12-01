using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelations;

[HandlerCode(HandlerCode.GetSymbolRelations)]
public sealed record GetSymbolRelationsRequest(string InvestorIsin, string InvestmentIsin) :
    PagingRequest, IRequest<Response<Paginated<GetSymbolRelationsResultItem>>>;