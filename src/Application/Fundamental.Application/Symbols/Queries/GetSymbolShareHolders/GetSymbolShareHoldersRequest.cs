using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Enums;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;

[HandlerCode(HandlerCode.GetSymbolShareHolders)]
public sealed record GetSymbolShareHoldersRequest(string? Isin, ShareHolderSource? Source, ReviewStatus? ReviewStatus) :
    PagingRequest,
    IRequest<Response<Paginated<GetSymbolShareHoldersResultDto>>>;