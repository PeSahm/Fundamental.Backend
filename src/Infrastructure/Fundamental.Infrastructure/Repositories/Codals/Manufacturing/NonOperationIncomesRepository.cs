using Fundamental.Application.Codals.Manufacturing.Queries.GetNonOperationIncomes;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public sealed class NonOperationIncomesRepository(FundamentalDbContext dbContext) : INonOperationIncomesRepository
{
    public async Task<Response<Paginated<GetNonOperationIncomesResultItem>>> GetNonOperationIncomesAsync(
        GetNonOperationIncomesRequest request,
        CancellationToken cancellationToken
    )
    {
        IQueryable<NonOperationIncomeAndExpense> query = from ex
                in dbContext.NonOperationIncomeAndExpenses.Include(x => x.Symbol)
                                                         where !ex.ForecastPeriod
                                                               && !ex.YearlyForecastPeriod
                                                               && (from ex2 in dbContext.NonOperationIncomeAndExpenses
                                                                   where ex2.ReportMonth.Month == ex.ReportMonth.Month
                                                                         && ex2.FiscalYear.Year == ex.FiscalYear.Year
                                                                         && !ex2.ForecastPeriod
                                                                         && !ex2.YearlyForecastPeriod
                                                                         && ex2.Symbol.Id == ex.Symbol.Id
                                                                   group ex2 by new { ex2.FiscalYear.Year, ex2.ReportMonth.Month, ex2.Symbol.Id }
                                                                   into g
                                                                   select g.Max(e => e.TraceNo)).Contains(ex.TraceNo)
                                                         select ex;

        if (request is { IsinList: not null } && request.IsinList.Length != 0)
        {
            query = query.Where(x => request.IsinList.Contains(x.Symbol.Isin));
        }

        if (request.Year.HasValue)
        {
            query = query.Where(x => x.FiscalYear.Year == request.Year);
        }

        if (request.ReportMonth.HasValue)
        {
            query = query.Where(x => x.ReportMonth.Month == request.ReportMonth);
        }

        query = request.TagStatus switch
        {
            NoneOperationalIncomeTagStatus.Tagged => query.Where(x => x.Tags.Any()),
            NoneOperationalIncomeTagStatus.Untagged => query.Where(x => !x.Tags.Any()),
            _ => query
        };

        Paginated<GetNonOperationIncomesResultItem> result = await query.Select(x => new GetNonOperationIncomesResultItem
        {
            Id = x.Id,
            Isin = x.Symbol.Isin,
            Symbol = x.Symbol.Name,
            Title = x.Symbol.Title,
            Uri = x.Uri,
            FiscalYear = x.FiscalYear.Year,
            YearEndMonth = x.YearEndMonth.Month,
            ReportMonth = x.ReportMonth.Month,
            TraceNo = x.TraceNo,
            Value = x.Value.Value,
            Description = x.Description,
            IsAudited = x.IsAudited,
            Tags = x.Tags
        })
            .ToPagingListAsync(request, "TraceNo desc", cancellationToken);
        return result;
    }
}