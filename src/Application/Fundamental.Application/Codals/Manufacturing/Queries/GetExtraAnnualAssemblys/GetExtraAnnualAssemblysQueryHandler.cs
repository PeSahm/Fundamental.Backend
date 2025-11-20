using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;

public sealed class GetExtraAnnualAssemblysQueryHandler(
    IExtraAnnualAssemblyRepository repository
) : IRequestHandler<GetExtraAnnualAssemblysRequest, Response<Paginated<GetExtraAnnualAssemblyListItem>>>
{
    /// <summary>
    /// Handles a request to retrieve extra annual assemblies and produces a paginated list of results.
    /// </summary>
    /// <param name="request">Query parameters for filtering and paging extra annual assemblies.</param>
    /// <param name="cancellationToken">Cancellation token to cancel the operation.</param>
    /// <returns>A Response containing a paginated list of <see cref="GetExtraAnnualAssemblyListItem"/>.</returns>
    public async Task<Response<Paginated<GetExtraAnnualAssemblyListItem>>> Handle(
        GetExtraAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    )
    {
        return await repository.GetExtraAnnualAssemblysAsync(request, cancellationToken);
    }
}