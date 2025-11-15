using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

public sealed class GetInterpretativeReportSummaryPage5sQueryHandler(
    IInterpretativeReportSummaryPage5Repository repository
) : IRequestHandler<GetInterpretativeReportSummaryPage5sRequest, Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>>
{
    public async Task<Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>> Handle(
        GetInterpretativeReportSummaryPage5sRequest request,
        CancellationToken cancellationToken)
    {
        return await repository.GetInterpretativeReportSummaryPage5sAsync(request, cancellationToken);
    }
}
