using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IExtraAnnualAssemblyRepository
{
    /// <summary>
    /// Retrieves a paginated list of extra annual assembly items matching the specified request.
    /// </summary>
    /// <param name="request">Filters and paging options for querying extra annual assembly items.</param>
    /// <returns>A paginated collection of <see cref="GetExtraAnnualAssemblyListItem"/>.</returns>
    Task<Paginated<GetExtraAnnualAssemblyListItem>> GetExtraAnnualAssemblysAsync(
        GetExtraAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    );
}