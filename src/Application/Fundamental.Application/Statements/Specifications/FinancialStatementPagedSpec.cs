using Ardalis.Specification;
using Fundamental.Application.Statements.Queries.GetFinancialStatements;
using Fundamental.Domain.Statements.Entities;

namespace Fundamental.Application.Statements.Specifications;

public sealed class FinancialStatementPagedSpec : Specification<FinancialStatement, GetFinancialStatementsResultItem>
{
    public FinancialStatementPagedSpec()
    {
        Query.Select(x => new GetFinancialStatementsResultItem(
            x.Symbol.Isin,
            x.Symbol.Name,
            x.Symbol.Title,
            x.TraceNo,
            x.Uri,
            x.FiscalYear.Year,
            x.YearEndMonth.Month,
            x.ReportMonth.Month,
            x.OperatingIncome.Value,
            x.GrossProfit.Value,
            x.OperatingProfit.Value,
            x.BankInterestIncome.Value,
            x.InvestmentIncome.Value,
            x.NetProfit.Value,
            x.Expense.Value,
            x.Asset.Value,
            x.OwnersEquity.Value,
            x.Receivables.Value
        ));
    }
}