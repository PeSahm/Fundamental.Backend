using Fundamental.Application.Symbols.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;

public sealed class GetSymbolShareHoldersQueryHandler(ISymbolShareHoldersReadRepository symbolRepository)
    : IRequestHandler<GetSymbolShareHoldersRequest, Response<Paginated<GetSymbolShareHoldersResultDto>>>
{
    public async Task<Response<Paginated<GetSymbolShareHoldersResultDto>>> Handle(
        GetSymbolShareHoldersRequest request,
        CancellationToken cancellationToken
    )
    {
        return await symbolRepository.GetShareHoldersAsync(request, cancellationToken);
    }
}