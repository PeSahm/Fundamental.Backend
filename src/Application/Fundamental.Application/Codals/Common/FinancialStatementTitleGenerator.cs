using System.Text;
using DNTPersianUtils.Core;
using Fundamental.Domain.Codals.ValueObjects;

namespace Fundamental.Application.Codals.Common;

public static class FinancialStatementTitleGenerator
{
    public static string GenerateTitle(
        string reportType,
        string symbol,
        ushort reportMonth,
        ushort yearEndMonth,
        ushort fiscalYear,
        bool isAudited
    )
    {
        string isAuditedDescription = isAudited ? "حسابرسی شده" : "حسابرسی نشده";

        return new StringBuilder(reportType)
            .Append(symbol)
            .Append(" ماه ")
            .Append(reportMonth)
            .Append(" دوره ")
            .Append(new StatementMonth(reportMonth).AdjustedMonth(yearEndMonth))
            .Append(" ماهه ")
            .Append(" منتهی به ")
            .Append($"{fiscalYear}/{yearEndMonth}/{GetLastDayOfFiscalYear(fiscalYear, yearEndMonth)}")
            .Append(' ')
            .Append(isAuditedDescription)
            .ToString();
    }

    private static int GetLastDayOfFiscalYear(ushort fiscalYear, ushort yearEndMonth)
    {
        DateOnly? dateNullable = $"{fiscalYear}/{yearEndMonth}/1".ToGregorianDateOnly();
        DateOnly date = dateNullable ?? DateOnly.FromDateTime(DateTime.Today);
        return date.GetPersianMonthStartAndEndDates().LastDayNumber;
    }
}