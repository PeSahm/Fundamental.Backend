using Fundamental.Application.Codals.Manufacturing.Queries.GetStatusOfViableCompanies;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public sealed class StatusOfViableCompaniesRepository(FundamentalDbContext dbContext) : IStatusOfViableCompaniesRepository
{
    public async Task<Response<Paginated<GetStatusOfViableCompaniesResultDto>>> GetStatusOfViableCompanies(
        GetStatusOfViableCompaniesRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<GetStatusOfViableCompaniesResultDto> query = dbContext.StockOwnership
            .Where(x => x.TraceNo == dbContext.BalanceSheets
                .Where(b => EF.Property<Guid>(b, "symbol-id") == EF.Property<Guid>(x, "parent_symbol_id"))
                .Max(t => t.TraceNo))
            .Select(x => new GetStatusOfViableCompaniesResultDto
            {
                Id = x.Id,
                CostPrice = x.CostPrice.Value,
                OwnershipPercentage = x.OwnershipPercentage,
                OwnershipPercentageProvidedByAdmin = x.OwnershipPercentageProvidedByAdmin,
                ReviewStatus = x.ReviewStatus,
                PatentSymbol = x.ParentSymbol.Name,
                ParentSymbolIsin = x.ParentSymbol.Isin,
                ParentSymbolName = x.ParentSymbol.Title,
                SubsidiarySymbolName = x.SubsidiarySymbolName,
                SubsidiarySymbolIsin = x.ParentSymbol.Isin,
                CreatedAt = x.CreatedAt,
                Url = x.Url,
            });

        if (!string.IsNullOrWhiteSpace(request.Isin))
        {
            query = query.Where(x => x.ParentSymbolIsin == request.Isin);
        }

        if (request.ReviewStatus.HasValue)
        {
            query = query.Where(x => x.ReviewStatus == request.ReviewStatus);
        }

        return await query.ToPagingListAsync(request, "CreatedAt desc", cancellationToken);
    }
}