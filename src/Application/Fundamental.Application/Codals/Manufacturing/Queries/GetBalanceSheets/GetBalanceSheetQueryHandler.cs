using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheets;

public sealed class GetBalanceSheetQueryHandler(IBalanceSheetReadRepository balanceSheetReadRepository)
    : IRequestHandler<GetBalanceSheetRequest, Response<Paginated<GetBalanceSheetResultDto>>>
{
    public async Task<Response<Paginated<GetBalanceSheetResultDto>>> Handle(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken
    )
    {
        return await balanceSheetReadRepository.GetBalanceSheet(request, cancellationToken);
    }
}