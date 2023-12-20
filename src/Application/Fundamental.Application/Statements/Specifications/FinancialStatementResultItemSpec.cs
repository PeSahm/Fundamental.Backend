using Ardalis.Specification;
using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

public sealed class FinancialStatementResultItemSpec : Specification<FinancialStatement, GetFinancialStatementsResultItem>
{
    public FinancialStatementResultItemSpec()
    {
        Query.Select(x => new GetFinancialStatementsResultItem(
            x.Id,
            x.Symbol.Isin,
            x.Symbol.Name,
            x.Symbol.Title,
            x.TraceNo,
            x.Uri,
            x.FiscalYear.Year,
            x.YearEndMonth.Month,
            x.ReportMonth.Month,
            (CodalMoney)x.OperatingIncome,
            (CodalMoney)x.GrossProfit,
            (CodalMoney)x.OperatingProfit,
            (CodalMoney)x.BankInterestIncome,
            (CodalMoney)x.InvestmentIncome,
            (CodalMoney)x.NetProfit,
            (CodalMoney)x.Expense,
            (CodalMoney)x.Asset,
            (CodalMoney)x.OwnersEquity,
            (CodalMoney)x.Receivables,
            x.CreatedAt
        ));
    }
}