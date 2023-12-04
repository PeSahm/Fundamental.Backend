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
            (CodalMoney)x.OperatingIncome.Value,
            (CodalMoney)x.GrossProfit.Value,
            (CodalMoney)x.OperatingProfit.Value,
            (CodalMoney)x.BankInterestIncome.Value,
            (CodalMoney)x.InvestmentIncome.Value,
            (CodalMoney)x.NetProfit.Value,
            (CodalMoney)x.Expense.Value,
            (CodalMoney)x.Asset.Value,
            (CodalMoney)x.OwnersEquity.Value,
            (CodalMoney)x.Receivables.Value
        ));
    }
}