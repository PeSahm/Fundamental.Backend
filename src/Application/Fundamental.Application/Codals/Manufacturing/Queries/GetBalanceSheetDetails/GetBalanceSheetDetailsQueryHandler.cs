using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetDetails;

public sealed class GetBalanceSheetDetailsQueryHandler(IRepository repository)
    : IRequestHandler<GetBalanceSheetDetailsRequest, Response<List<GetBalanceSheetDetailResultDto>>>
{
    public async Task<Response<List<GetBalanceSheetDetailResultDto>>> Handle(
        GetBalanceSheetDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        List<GetBalanceSheetDetailResultDto> data = await repository.ListAsync(
            BalanceSheetDetailSpec.Where(request.TraceNo, request.FiscalYear, request.ReportMonth),
            cancellationToken);

        return data;
    }
}