using Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2.
    NonOperationIncomeAndExpenses;
using Newtonsoft.Json;

namespace Fundamental.Application.Codal.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage5Summaries.V2;
#pragma warning disable SA1402

public class RootInterpretativeReportSummaryPage5
{
    [JsonProperty("listedCapital")]
    public string ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; }

    [JsonProperty("interpretativeReportSummaryPage5")]
    public CodalInterpretativeReportSummaryPage5 InterpretativeReportSummaryPage5 { get; set; }
}

public class CodalInterpretativeReportSummaryPage5
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("nonOperationIncomeAndExpensesInvestmentIncome")]
    public CodalNonOperationIncomeAndExpenses? NonOperationIncomeAndExpenses { get; set; }
}
#pragma warning restore SA1402