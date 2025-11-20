using Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblys;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface IExtraAssemblyRepository
{
    Task<Paginated<GetExtraAssemblyListItem>> GetExtraAssemblysAsync(
        GetExtraAssemblysRequest request,
        CancellationToken cancellationToken
    );
}
