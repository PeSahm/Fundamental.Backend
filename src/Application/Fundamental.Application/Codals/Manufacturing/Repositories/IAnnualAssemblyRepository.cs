using Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblys;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IAnnualAssemblyRepository
{
    Task<Paginated<GetAnnualAssemblyListItem>> GetAnnualAssemblysAsync(
        GetAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    );
}
