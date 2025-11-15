using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

public sealed class GetInterpretativeReportSummaryPage5sQueryHandler(
    FundamentalDbContext dbContext
) : IRequestHandler<GetInterpretativeReportSummaryPage5sRequest, Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>>
{
    public async Task<Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>> Handle(
        GetInterpretativeReportSummaryPage5sRequest request,
        CancellationToken cancellationToken)
    {
        IQueryable<GetInterpretativeReportSummaryPage5ListItem> query = dbContext.CanonicalInterpretativeReportSummaryPage5s
            .AsNoTracking()
            .Where(x =>
                (request.Isin == null || x.Symbol.Isin == request.Isin) &&
                (request.FiscalYear == null || x.FiscalYear.Year == request.FiscalYear) &&
                (request.ReportMonth == null || x.ReportMonth.Month == request.ReportMonth))
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .Select(x => new GetInterpretativeReportSummaryPage5ListItem(
                x.Id,
                x.Symbol.Isin,
                x.Symbol.SymbolName,
                x.Symbol.Title,
                x.Uri,
                x.Version,
                x.FiscalYear.Year,
                x.YearEndMonth.Month,
                x.ReportMonth.Month,
                x.TraceNo,
                x.PublishDate
            ));

        int totalCount = await query.CountAsync(cancellationToken);

        List<GetInterpretativeReportSummaryPage5ListItem> items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);

        return Response.Successful(new Paginated<GetInterpretativeReportSummaryPage5ListItem>(
            items,
            totalCount,
            request.PageNumber,
            request.PageSize
        ));
    }
}
