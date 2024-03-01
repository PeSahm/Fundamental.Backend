using Ardalis.Specification;
using Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;
using Fundamental.Domain.Codals;

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
            OperatingIncome = x.OperatingIncome,
            GrossProfit = x.GrossProfit,
            OperatingProfit = x.OperatingProfit,
            BankInterestIncome = x.BankInterestIncome,
            InvestmentIncome = x.InvestmentIncome,
            NetProfit = x.NetProfit,
            Expense = x.Expense,
            Asset = x.Asset,
            OwnersEquity = x.OwnersEquity,
            Receivables = x.Receivables,
            CreatedAt = x.CreatedAt,
            UpdatedAt = x.UpdatedAt
        });
    }
}