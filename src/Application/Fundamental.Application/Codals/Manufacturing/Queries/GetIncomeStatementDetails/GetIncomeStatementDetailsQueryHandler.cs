using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementDetails;

public sealed class GetIncomeStatementDetailsQueryHandler(IRepository repository)
    : IRequestHandler<GetIncomeStatementDetailsRequest, Response<List<GetIncomeStatementDetailsResultDto>>>
{
    public async Task<Response<List<GetIncomeStatementDetailsResultDto>>> Handle(
        GetIncomeStatementDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        List<GetIncomeStatementDetailsResultDto> data = await repository.ListAsync(
            IncomeStatementDetailSpec.Where(request.TraceNo, request.FiscalYear, request.ReportMonth),
            cancellationToken);

        return data;
    }
}