using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;

public sealed class SimpleBalanceSheet
{
    public string Isin { get; set; } = null!;
    public ulong TraceNo { get; set; }
    public FiscalYear FiscalYear { get; set; }
    public StatementMonth ReportMonth { get; set; }
    public StatementMonth YearEndMonth { get; set; }
}