using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class ExtraAnnualAssemblyRepository(FundamentalDbContext dbContext) : IExtraAnnualAssemblyRepository
{
    /// <summary>
    /// Retrieves a paginated list of extra annual assembly list items applying optional filters from the request.
    /// </summary>
    /// <param name="request">Filtering and paging parameters. Supported filters: <c>Isin</c>, <c>FiscalYear</c>, and <c>YearEndMonth</c>; also contains paging and sorting options.</param>
    /// <param name="cancellationToken">Token to cancel the operation.</param>
    /// <returns>A paginated collection of <see cref="GetExtraAnnualAssemblyListItem"/> matching the provided filters, ordered by fiscal year descending then assembly date descending by default.</returns>
    public async Task<Paginated<GetExtraAnnualAssemblyListItem>> GetExtraAnnualAssemblysAsync(
        GetExtraAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<CanonicalExtraAnnualAssembly> query = dbContext.CanonicalExtraAnnualAssemblies
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

        return await query.Select(x => new GetExtraAnnualAssemblyListItem
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