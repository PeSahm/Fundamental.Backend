namespace Fundamental.Application.Statements.Queries.GetFinancialStatements;

public record GetFinancialStatementsResultItem(
    Guid Id,
    string Isin,
    string Symbol,
    string Title,
    ulong TraceNo,
    string Uri,
    ushort FiscalYear,
    ushort YearEndMonth,
    ushort ReportMonth,
    decimal OperatingIncome,
    decimal GrossProfit,
    decimal OperatingProfit,
    decimal BankInterestIncome,
    decimal InvestmentIncome,
    decimal NetProfit,
    decimal Expense,
    decimal Asset,
    decimal OwnersEquity,
    decimal Receivables,
    DateTime CreatedAt
);