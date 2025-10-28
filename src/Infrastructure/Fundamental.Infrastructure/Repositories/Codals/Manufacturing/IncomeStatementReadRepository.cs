using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

public sealed class IncomeStatementReadRepository(FundamentalDbContext dbContext) : IIncomeStatementsReadRepository
{
    public async Task<Paginated<GetIncomeStatementsResultDto>> GetIncomeStatements(
        GetIncomeStatementsRequest request,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<IncomeStatement> query = dbContext.IncomeStatements.Include(x => x.Details).Where(x => x.Details.Any(d => d.Description != null)).AsNoTracking();

        if (request is { IsinList: not null } && request.IsinList.Count != 0)
        {
            query = query.Where(x => request.IsinList.Contains(x.Symbol.Isin));
        }

        if (request.FiscalYear.HasValue)
        {
            query = query.Where(x => x.FiscalYear.Year == request.FiscalYear);
        }

        if (request.ReportMonth.HasValue)
        {
            query = query.Where(x => x.ReportMonth.Month == request.ReportMonth);
        }

        if (request.TraceNo.HasValue)
        {
            query = query.Where(x => x.TraceNo == request.TraceNo);
        }

        Paginated<GetIncomeStatementsResultDto> validStatements = await query
            .Where(x => x.ReportMonth.Month != 1)
            .GroupBy(gb => new { gb.Symbol.Isin, FiscalYear = gb.FiscalYear.Year, ReportMonth = gb.ReportMonth.Month })
            .Select(x => new GetIncomeStatementsResultDto
            {
                Isin = x.Key.Isin,
                TraceNo = x.Max(mx => mx.TraceNo),
                FiscalYear = x.Key.FiscalYear,
                ReportMonth = x.Key.ReportMonth,
                IsAudited = x.First().IsAudited,
                YearEndMonth = x.First().YearEndMonth.Month,
                Symbol = x.First().Symbol.Name,
                PublishDate = x.Max(mx => mx.PublishDate),
                Uri = x.First().Uri
            }).ToPagingListAsync(request, "PublishDate desc", cancellationToken);

        return validStatements;
    }

    public Task<SignedCodalMoney?> GetLastIncomeStatement(
        string isin,
        FiscalYear fiscalYear,
        StatementMonth statementMonth,
        ushort incomeStatementRow,
        CancellationToken cancellationToken = default
    )
    {
        return dbContext.IncomeStatements
            .Include(x => x.Details)
            .AsNoTracking()
            .Where(x =>
                x.Symbol.Isin == isin &&
                x.FiscalYear.Year <= fiscalYear &&
                x.ReportMonth.Month <= statementMonth)
            .SelectMany(x => x.Details)
            .Where(d => d.CodalRow == incomeStatementRow)
            .OrderByDescending(x => x.IncomeStatement.FiscalYear.Year)
            .ThenByDescending(x => x.IncomeStatement.ReportMonth.Month)
            .ThenByDescending(x => x.IncomeStatement.TraceNo)
            .Select(x => x.Value)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<(FiscalYear Year, StatementMonth Month, ulong TraceNo)> GetLatestStatement(string isin, CancellationToken cancellationToken = default)
    {
        return dbContext.IncomeStatements
            .AsNoTracking()
            .Where(x => x.Symbol.Isin == isin)
            .OrderByDescending(x => x.FiscalYear.Year)
            .ThenByDescending(x => x.ReportMonth.Month)
            .ThenByDescending(x => x.TraceNo)
            .Select(x => new ValueTuple<FiscalYear, StatementMonth, ulong>(x.FiscalYear, x.ReportMonth, x.TraceNo))
            .FirstOrDefaultAsync(cancellationToken);
    }
}