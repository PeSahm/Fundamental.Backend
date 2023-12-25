using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Specifications;

public sealed class FinancialStatementResultItemSpec : Specification<FinancialStatement, GetFinancialStatementsResultItem>
{
    public FinancialStatementResultItemSpec()
    {
        Query.Select(x => new GetFinancialStatementsResultItem
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
        });
    }
}