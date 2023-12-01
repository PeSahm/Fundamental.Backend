using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Application.Statements.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.Entities;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories;

public class FinancialStatementsReadRepository : IFinancialStatementsReadRepository
{
    private readonly FundamentalDbContext _dbContext;

    public FinancialStatementsReadRepository(FundamentalDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<Paginated<GetFinancialStatementsResultItem>> GetFinancialStatementsAsync(
        GetFinancialStatementsRequest request,
        CancellationToken cancellationToken = default
    )
    {
        IQueryable<FinancialStatement> query = _dbContext.FinancialStatements.AsNoTracking();

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

        return query.Select(x => new GetFinancialStatementsResultItem(
            x.Id,
            x.Symbol.Isin,
            x.Symbol.Name,
            x.Symbol.Title,
            x.TraceNo,
            x.Uri,
            x.FiscalYear,
            x.YearEndMonth,
            x.ReportMonth,
            (CodalMoney)x.OperatingIncome,
            (CodalMoney)x.GrossProfit,
            (CodalMoney)x.OperatingProfit,
            (CodalMoney)x.BankInterestIncome,
            (CodalMoney)x.InvestmentIncome,
            (CodalMoney)x.NetProfit,
            (CodalMoney)x.Expense,
            (CodalMoney)x.Asset,
            (CodalMoney)x.OwnersEquity,
            (CodalMoney)x.Receivables
        )).ToPagingListAsync(request, cancellationToken);
    }
}