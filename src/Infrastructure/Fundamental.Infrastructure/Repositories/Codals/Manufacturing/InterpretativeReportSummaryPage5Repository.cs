using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Dto;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public class InterpretativeReportSummaryPage5Repository(FundamentalDbContext dbContext) : IInterpretativeReportSummaryPage5Repository
{
    public async Task<Paginated<GetInterpretativeReportSummaryPage5ListItem>> GetInterpretativeReportSummaryPage5sAsync(
        GetInterpretativeReportSummaryPage5SRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<CanonicalInterpretativeReportSummaryPage5> query = dbContext.CanonicalInterpretativeReportSummaryPage5s
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

        if (request.ReportMonth.HasValue)
        {
            query = query.Where(x => x.ReportMonth.Month == request.ReportMonth);
        }

        return await query.Select(x => new GetInterpretativeReportSummaryPage5ListItem
        {
            Id = x.Id,
            Isin = x.Symbol.Isin,
            Symbol = x.Symbol.Name,
            Title = x.Symbol.Title,
            Uri = x.Uri,
            Version = x.Version,
            FiscalYear = x.FiscalYear.Year,
            YearEndMonth = x.YearEndMonth.Month,
            ReportMonth = x.ReportMonth.Month,
            TraceNo = x.TraceNo,
            PublishDate = x.PublishDate
        })
            .ToPagingListAsync(request, "FiscalYear desc, ReportMonth desc", cancellationToken);
    }
}