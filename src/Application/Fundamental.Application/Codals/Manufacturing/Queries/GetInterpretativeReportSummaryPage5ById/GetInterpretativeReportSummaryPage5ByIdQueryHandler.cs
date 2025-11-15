using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetInterpretativeReportSummaryPage5ById;

public sealed class GetInterpretativeReportSummaryPage5ByIdQueryHandler(
    IRepository repository
) : IRequestHandler<GetInterpretativeReportSummaryPage5ByIdRequest, Response<GetInterpretativeReportSummaryPage5DetailItem>>
{
    public async Task<Response<GetInterpretativeReportSummaryPage5DetailItem>> Handle(
        GetInterpretativeReportSummaryPage5ByIdRequest request,
        CancellationToken cancellationToken)
    {
        GetInterpretativeReportSummaryPage5DetailItem? result = await repository.FirstOrDefaultAsync(
            new InterpretativeReportSummaryPage5DetailItemSpec()
                .WhereId(request.Id),
            cancellationToken
        );

        if (result is null)
        {
            return GetInterpretativeReportSummaryPage5ByIdErrorCodes.InterpretativeReportSummaryPage5NotFound;
        }

        return result;
    }
}
