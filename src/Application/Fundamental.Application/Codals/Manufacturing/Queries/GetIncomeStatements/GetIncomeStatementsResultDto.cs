using Fundamental.Application.Codals.Common;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatements;

public sealed class GetIncomeStatementsResultDto
{
    public Guid Id { get; set; }
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public ulong TraceNo { get; init; }
    public string Uri { get; init; }
    public ushort FiscalYear { get; init; }
    public ushort YearEndMonth { get; init; }
    public ushort ReportMonth { get; init; }
    public bool IsAudited { get; init; }
    public string IsAuditedDescription => IsAudited ? "حسابرسی شده" : "حسابرسی نشده";

    public string Title => FinancialStatementTitleGenerator.GenerateTitle(
        "گزارش صورت سود و زیان نماد ",
        Symbol,
        ReportMonth,
        YearEndMonth,
        FiscalYear,
        IsAudited);
    public DateTime PublishDate { get; init; }
}