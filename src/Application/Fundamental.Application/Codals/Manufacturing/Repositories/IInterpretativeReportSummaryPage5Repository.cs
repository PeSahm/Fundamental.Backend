using Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IInterpretativeReportSummaryPage5Repository
{
    Task<Paginated<GetInterpretativeReportSummaryPage5ListItem>> GetInterpretativeReportSummaryPage5sAsync(
        GetInterpretativeReportSummaryPage5SRequest request,
        CancellationToken cancellationToken
    );
}