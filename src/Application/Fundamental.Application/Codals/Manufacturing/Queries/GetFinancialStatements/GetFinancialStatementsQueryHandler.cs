using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;

public sealed class GetFinancialStatementsQueryHandler(IRepository repository)
    : IRequestHandler<GetFinancialStatementsReqeust, Response<GetFinancialStatementsResultDto>>
{
    public async Task<Response<GetFinancialStatementsResultDto>> Handle(
        GetFinancialStatementsReqeust request,
        CancellationToken cancellationToken
    )
    {
        GetFinancialStatementsResultDto? result =
            await repository.FirstOrDefaultAsync(
                new FinancialStatementsSpec()
                    .WhereIsin(request.Isin)
                    .ToResultDto()
                    .OrderByLastRecord(),
                cancellationToken);

        if (result is null)
        {
            return GetFinancialStatementsErrorCodes.RecodeNotFound;
        }

        return result;
    }
}