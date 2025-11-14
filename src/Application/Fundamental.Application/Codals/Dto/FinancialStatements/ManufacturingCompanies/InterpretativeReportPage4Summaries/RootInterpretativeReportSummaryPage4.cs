using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries.V2.
    TheStatusOfViableCompanies;
using Newtonsoft.Json;

namespace Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries;

#pragma warning disable SA1402
public class RootInterpretativeReportSummaryPage4
{
    [JsonProperty("listedCapital")]
    public string ListedCapital { get; set; }

    [JsonProperty("unauthorizedCapital")]
    public string UnauthorizedCapital { get; set; }

    [JsonProperty("interpretativeReportSummaryPage4")]
    public CodalInterpretativeReportSummaryPage4 InterpretativeReportSummaryPage4 { get; set; }
}

public class CodalInterpretativeReportSummaryPage4
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("theStatusOfViableCompanies")]
    public CodalStatusOfViableCompanies? TheStatusOfViableCompanies { get; set; }
}
#pragma warning restore SA1402