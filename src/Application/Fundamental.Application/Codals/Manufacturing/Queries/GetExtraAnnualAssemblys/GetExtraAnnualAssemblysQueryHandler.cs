using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;

public sealed class GetExtraAnnualAssemblysQueryHandler(
    IAnnualAssemblyRepository repository
) : IRequestHandler<GetExtraAnnualAssemblysRequest, Response<Paginated<GetExtraAnnualAssemblyListItem>>>
{
    public async Task<Response<Paginated<GetExtraAnnualAssemblyListItem>>> Handle(
        GetExtraAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetExtraAnnualAssemblysAsync(request, cancellationToken);
    }
}
