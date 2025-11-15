using Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class AnnualAssemblyRepository(FundamentalDbContext dbContext) : IAnnualAssemblyRepository
{
    public async Task<Paginated<GetAnnualAssemblyListItem>> GetAnnualAssemblysAsync(
        GetAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<CanonicalAnnualAssembly> query = dbContext.CanonicalAnnualAssemblies
            .AsNoTracking()
            .Include(x => x.Symbol);

        if (!string.IsNullOrWhiteSpace(request.Isin))
        {
            query = query.Where(x => x.Symbol.Isin == request.Isin);
        }

        if (request.FiscalYear.HasValue)
        {
            query = query.Where(x => x.FiscalYear.Year == request.FiscalYear);
        }

        if (request.YearEndMonth.HasValue)
        {
            query = query.Where(x => x.YearEndMonth.Month == request.YearEndMonth);
        }

        return await query.Select(x => new GetAnnualAssemblyListItem
            {
                Id = x.Id,
                Isin = x.Symbol.Isin,
                Symbol = x.Symbol.Name,
                Title = x.Symbol.Title,
                HtmlUrl = x.HtmlUrl.ToString(),
                Version = x.Version,
                FiscalYear = x.FiscalYear.Year,
                YearEndMonth = x.YearEndMonth.Month,
                AssemblyDate = x.AssemblyDate,
                TraceNo = x.TraceNo,
                PublishDate = x.PublishDate,
                AssemblyResultTypeTitle = x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.AssemblyResultTypeTitle ?? string.Empty : string.Empty
            })
            .ToPagingListAsync(request, "FiscalYear desc, AssemblyDate desc", cancellationToken);
    }
}
