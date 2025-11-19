using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblys;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IExtraAnnualAssemblyRepository
{
    Task<Paginated<GetExtraAnnualAssemblyListItem>> GetExtraAnnualAssemblysAsync(
        GetExtraAnnualAssemblysRequest request,
        CancellationToken cancellationToken
    );
}
