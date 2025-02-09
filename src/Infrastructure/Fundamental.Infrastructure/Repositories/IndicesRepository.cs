using Fundamental.Application.Symbols.Queries.GetIndices;
using Fundamental.Application.Symbols.Repositories;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Index = Fundamental.Domain.Symbols.Entities.Index;

namespace Fundamental.Infrastructure.Repositories;

public class IndicesRepository(FundamentalDbContext fundamentalDbContext) : IIndicesRepository
{
    public async Task<GetIndicesResultDto> GetIndices(
        GetIndicesRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<Index> query = from index in fundamentalDbContext.Indices
            where index.Symbol.Isin == request.Isin
            select index;

        if (request.From.HasValue)
        {
            query = query.Where(x => x.Date >= request.From);
        }

        if (request.To.HasValue)
        {
            query = query.Where(x => x.Date <= request.To);
        }

        List<Index> data = await query.OrderBy(x => x.Date)
            .AsNoTracking()
            .ToListAsync(cancellationToken: cancellationToken);

        if (data.Count == 0)
        {
            return new GetIndicesResultDto
            {
                Name = await fundamentalDbContext.Symbols.Where(x => x.Isin == request.Isin).Select(x => x.Name)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? "Symbol Not found"
            };
        }

        return new GetIndicesResultDto
        {
            Isin = request.Isin,
            Name = data[0].Symbol.Name,
            TseInsCode = data[0].Symbol.TseInsCode,
            Average = data.Average(x => x.Value),
            Data = data.Select(x => new GetIndicesResulItem
            {
                Value = x.Value,
                Date = x.Date
            }).ToList()
        }.CalculatePercentage();
    }
}