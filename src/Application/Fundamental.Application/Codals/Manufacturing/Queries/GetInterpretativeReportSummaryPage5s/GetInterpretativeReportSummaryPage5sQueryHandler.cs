using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5s;

public sealed class GetInterpretativeReportSummaryPage5SQueryHandler(
    IInterpretativeReportSummaryPage5Repository repository
) : IRequestHandler<GetInterpretativeReportSummaryPage5SRequest, Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>>
{
    public async Task<Response<Paginated<GetInterpretativeReportSummaryPage5ListItem>>> Handle(
        GetInterpretativeReportSummaryPage5SRequest request,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetInterpretativeReportSummaryPage5sAsync(request, cancellationToken);
    }
}