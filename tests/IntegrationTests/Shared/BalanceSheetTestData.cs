using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;

namespace IntegrationTests.Shared;

/// <summary>
/// Data structures and parsing utilities for balance sheet test data.
/// </summary>
public static class BalanceSheetTestData
{
    /// <summary>
    /// Represents expected balance sheet data from CSV files.
    /// </summary>
    public record BalanceSheetExpectation(
        int SymbolId,
        ulong TraceNo,
        string Uri,
        string Currency,
        int YearEndMonth,
        int ReportMonth,
        int Row,
        int CodalRow,
        BalanceSheetCategory CodalCategory,
        string? Description,
        bool IsAudited,
        int FiscalYear,
        decimal Value,
        DateTime CreatedAt,
        DateTime ModifiedAt,
        int FinancialStatementId);

    /// <summary>
    /// Parses CSV file containing expected balance sheet results.
    /// </summary>
    /// <param name="csvFilePath">The path to the CSV file containing expected balance sheet data.</param>
    public static async Task<List<BalanceSheetExpectation>> ParseCsvExpectations(string csvFilePath)
    {
        List<BalanceSheetExpectation> expectations = new();
        string[] lines = await File.ReadAllLinesAsync(csvFilePath);

        // Skip header line
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            string[] fields = line.Split(',');

            if (fields.Length < 17)
            {
                continue;
            }

            // Handle empty strings for integer fields
            int symbolId = string.IsNullOrWhiteSpace(fields[2]) ? 0 : int.Parse(fields[2]);
            ulong traceNo = string.IsNullOrWhiteSpace(fields[3]) ? 0 : ulong.Parse(fields[3]);
            int yearEndMonth = string.IsNullOrWhiteSpace(fields[6]) ? 0 : int.Parse(fields[6]);
            int reportMonth = string.IsNullOrWhiteSpace(fields[7]) ? 0 : int.Parse(fields[7]);
            int row = string.IsNullOrWhiteSpace(fields[8]) ? 0 : int.Parse(fields[8]);
            int codalRow = string.IsNullOrWhiteSpace(fields[9]) ? 0 : int.Parse(fields[9]);
            int codalCategoryValue = string.IsNullOrWhiteSpace(fields[10]) ? 0 : int.Parse(fields[10]);
            int fiscalYear = string.IsNullOrWhiteSpace(fields[13]) ? 0 : int.Parse(fields[13]);
            decimal value = string.IsNullOrWhiteSpace(fields[14]) ? 0 : decimal.Parse(fields[14]);
            int financialStatementId = fields.Length > 17 && !string.IsNullOrWhiteSpace(fields[17]) ? int.Parse(fields[17]) : 0;

            BalanceSheetExpectation expectation = new(
                SymbolId: symbolId,
                TraceNo: traceNo,
                Uri: fields[4],
                Currency: fields[5],
                YearEndMonth: yearEndMonth,
                ReportMonth: reportMonth,
                Row: row,
                CodalRow: codalRow,
                codalCategoryValue == 1 ? BalanceSheetCategory.Assets : BalanceSheetCategory.Liability,
                Description: fields[11] == string.Empty ? null : fields[11],
                IsAudited: !string.IsNullOrWhiteSpace(fields[12]) && bool.Parse(fields[12]),
                FiscalYear: fiscalYear,
                Value: value,
                CreatedAt: DateTime.Parse(fields[15], System.Globalization.CultureInfo.InvariantCulture),
                ModifiedAt: DateTime.Parse(fields[16], System.Globalization.CultureInfo.InvariantCulture),
                FinancialStatementId: financialStatementId);

            expectations.Add(expectation);
        }

        return expectations;
    }
}