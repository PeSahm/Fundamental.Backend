using Fundamental.Application.Symbols.Queries;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories;

public class SymbolRelationRepository : ISymbolRelationRepository
{
    private readonly FundamentalDbContext _dbContext;

    public SymbolRelationRepository(FundamentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Paginated<GetSymbolRelationsResultItem>> GetSymbolRelations(
        GetSymbolRelationsRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<SymbolRelation> query = _dbContext.SymbolRelations.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(request.InvestorIsin))
        {
            query = query.Where(x => x.Parent.Isin == request.InvestorIsin);
        }

        return await query.Select(x => new GetSymbolRelationsResultItem(
            x.Parent.Isin,
            x.Parent.Name,
            x.Parent.Title,
            x.Child.Isin,
            x.Child.Name,
            x.Child.Title,
            x.Ratio
        )).ToPagingListAsync(request, cancellationToken);
    }
}