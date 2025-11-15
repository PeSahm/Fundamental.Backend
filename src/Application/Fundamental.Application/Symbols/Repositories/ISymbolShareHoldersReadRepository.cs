using Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Symbols.Repositories;

public interface ISymbolShareHoldersReadRepository
{
    Task<Paginated<GetSymbolShareHoldersResultDto>> GetShareHoldersAsync(
        GetSymbolShareHoldersRequest request,
        CancellationToken cancellationToken = default
    );
}