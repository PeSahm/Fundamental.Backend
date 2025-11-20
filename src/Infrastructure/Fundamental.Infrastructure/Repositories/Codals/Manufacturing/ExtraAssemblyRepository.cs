using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblys;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class ExtraAssemblyRepository(FundamentalDbContext dbContext) : IExtraAssemblyRepository
{
    public async Task<Paginated<GetExtraAssemblyListItem>> GetExtraAssemblysAsync(
        GetExtraAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<CanonicalExtraAssembly> query = dbContext.CanonicalExtraAssemblies
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

        return await query.Select(x => new GetExtraAssemblyListItem
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
            CapitalChangeState = x.CapitalChangeState,
            AssemblyResultTypeTitle = x.ParentAssemblyInfo != null ? x.ParentAssemblyInfo.AssemblyResultTypeTitle ?? string.Empty : string.Empty
        })
            .ToPagingListAsync(request, "FiscalYear desc, AssemblyDate desc", cancellationToken);
    }
}
