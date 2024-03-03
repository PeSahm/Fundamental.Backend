using Fundamental.Application.Services.Models;

namespace Fundamental.Application.Services;

public interface ICodalReportingService
{
    List<CodalValidTraceNoResultDto> GetValidBalanceSheetReports(string isin);

    List<CodalValidTraceNoResultDto> GetValidIncomeStatementReports(string isin);
}