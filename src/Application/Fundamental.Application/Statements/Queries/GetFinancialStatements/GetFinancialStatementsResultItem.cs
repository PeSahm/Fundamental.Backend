namespace Fundamental.Application.Statements.Queries.GetFinancialStatements;

public class GetFinancialStatementsResultItem
{
    public Guid Id { get; init; }
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public string Title { get; init; }
    public ulong TraceNo { get; init; }
    public string Uri { get; init; }
    public ushort FiscalYear { get; init; }
    public ushort YearEndMonth { get; init; }
    public ushort ReportMonth { get; init; }
    public decimal OperatingIncome { get; init; }
    public decimal GrossProfit { get; init; }
    public decimal OperatingProfit { get; init; }
    public decimal BankInterestIncome { get; init; }
    public decimal InvestmentIncome { get; init; }
    public decimal NetProfit { get; init; }
    public decimal Expense { get; init; }
    public decimal Asset { get; init; }
    public decimal OwnersEquity { get; init; }
    public decimal Receivables { get; init; }
    public required DateTime CreatedAt { get; init; }
    public required DateTime? UpdatedAt { get; init; }
}