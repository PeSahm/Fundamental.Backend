using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Statements.Commands.UpdateFinancialStatement;

[HandlerCode(HandlerCode.UpdateFinancialStatement)]
public record UpdateFinancialStatementRequest(
    Guid Id,
    string Isin,
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
    decimal Receivables
) : IRequest<Response>;