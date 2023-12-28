using System.Text;
using DNTPersianUtils.Core;

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

    public string Title => new StringBuilder("گزارش صورت سود و زیان نماد ")
        .Append(Symbol)
        .Append(" دوره ")
        .Append(ReportMonth)
        .Append(" ماهه ")
        .Append(" منتهی به ")
        .Append($"{FiscalYear}/{YearEndMonth}/{GetLastDayOfFiscalYear()}")
        .Append(' ')
        .Append(IsAuditedDescription)
        .ToString();

    private int GetLastDayOfFiscalYear()
    {
        int lastDay = $"{FiscalYear}/{YearEndMonth}/1".ToGregorianDateOnly().GetPersianMonthStartAndEndDates()!.LastDayNumber;
        return lastDay;
    }
}