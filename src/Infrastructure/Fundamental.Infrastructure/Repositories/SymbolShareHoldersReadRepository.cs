using Fundamental.Application.Symbols.Queries.GetSymbolShareHolders;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;

namespace Fundamental.Infrastructure.Repositories;

public class SymbolShareHoldersReadRepository(FundamentalDbContext dbContext) : ISymbolShareHoldersReadRepository
{
    public Task<Paginated<GetSymbolShareHoldersResultDto>> GetShareHoldersAsync(
        GetSymbolShareHoldersRequest request,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<GetSymbolShareHoldersResultDto> query = dbContext.SymbolShareHolders
            .Select(x => new GetSymbolShareHoldersResultDto
            {
                Id = x.Id,
                SymbolIsin = x.Symbol.Isin,
                SymbolName = x.Symbol.Name,
                ShareHolderName = x.ShareHolderName,
                ReviewStatus = x.ReviewStatus,
                SharePercentage = x.SharePercentage,
                ShareHolderSymbolIsin = x.ShareHolderSymbol!.Isin,
                ShareHolderSymbolName = x.ShareHolderSymbol!.Name
            });

        if (!string.IsNullOrWhiteSpace(request.Isin))
        {
            query = query.Where(x => x.SymbolIsin == request.Isin);
        }

        if (request.ReviewStatus.HasValue)
        {
            query = query.Where(x => x.ReviewStatus == request.ReviewStatus);
        }

        return query.ToPagingListAsync(request, "ReviewStatus asc", cancellationToken);
    }
}