using Fundamental.Application.Symbols.Queries.GetSymbolRelations;
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

        if (!string.IsNullOrWhiteSpace(request.InvestmentIsin))
        {
            query = query.Where(x => x.Child.Isin == request.InvestmentIsin);
        }

        return await query.Select(x => new GetSymbolRelationsResultItem
            {
                Id = x.Id,
                InvestorIsin = x.Parent.Isin,
                InvestorSymbol = x.Parent.Name,
                InvestorTitle = x.Parent.Title,
                InvestmentSymbol = x.Child.Name,
                InvestmentTitle = x.Child.Title,
                Ratio = x.Ratio
            })
            .ToPagingListAsync(request, "InvestorIsin desc", cancellationToken);
    }
}