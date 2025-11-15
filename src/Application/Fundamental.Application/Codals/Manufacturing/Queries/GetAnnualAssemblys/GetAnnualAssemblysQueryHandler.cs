using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;

public sealed class GetAnnualAssemblysQueryHandler(
    IAnnualAssemblyRepository repository
) : IRequestHandler<GetAnnualAssemblysRequest, Response<Paginated<GetAnnualAssemblyListItem>>>
{
    public async Task<Response<Paginated<GetAnnualAssemblyListItem>>> Handle(
        GetAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetAnnualAssemblysAsync(request, cancellationToken);
    }
}
