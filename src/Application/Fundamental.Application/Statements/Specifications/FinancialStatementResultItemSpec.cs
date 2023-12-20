using Ardalis.Specification;
using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

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
            CreatedAt = x.CreatedAt
        });
    }
}