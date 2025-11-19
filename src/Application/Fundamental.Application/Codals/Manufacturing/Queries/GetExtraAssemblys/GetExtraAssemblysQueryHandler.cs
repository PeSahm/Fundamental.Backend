using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblys;

public sealed class GetExtraAssemblysQueryHandler(
    IExtraAssemblyRepository repository
) : IRequestHandler<GetExtraAssemblysRequest, Response<Paginated<GetExtraAssemblyListItem>>>
{
    public async Task<Response<Paginated<GetExtraAssemblyListItem>>> Handle(
        GetExtraAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetExtraAssemblysAsync(request, cancellationToken);
    }
}
