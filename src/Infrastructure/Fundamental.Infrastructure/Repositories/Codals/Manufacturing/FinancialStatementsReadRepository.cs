using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.Dto;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Infrastructure.Extensions;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Repositories.Codals.Manufacturing;

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

        return query.Select(x => new GetFinancialStatementsResultItem
            {
                Id = x.Id,
                Isin = x.Symbol.Isin,
                Symbol = x.Symbol.Name,
                Title = x.Symbol.Title,
                TraceNo = x.TraceNo,
                Uri = x.Uri,
                FiscalYear = x.FiscalYear.Year,
                YearEndMonth = x.FiscalYear.Year,
                ReportMonth = x.ReportMonth.Month,
                OperatingIncome = (CodalMoney)x.OperatingIncome,
                GrossProfit = (CodalMoney)x.GrossProfit,
                OperatingProfit = (CodalMoney)x.OperatingProfit,
                BankInterestIncome = (CodalMoney)x.BankInterestIncome,
                InvestmentIncome = (CodalMoney)x.InvestmentIncome,
                NetProfit = (CodalMoney)x.NetProfit,
                Expense = (CodalMoney)x.Expense,
                Asset = (CodalMoney)x.Asset,
                OwnersEquity = (CodalMoney)x.OwnersEquity,
                Receivables = (CodalMoney)x.Receivables,
                CreatedAt = x.CreatedAt,
                UpdatedAt = x.UpdatedAt
            })
            .ToPagingListAsync(request, "UpdatedAt desc", cancellationToken);
    }
}