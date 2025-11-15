using Fundamental.Application.Symbols.Queries.GetIndices;

namespace Fundamental.Application.Symbols.Repositories;

public interface IIndicesRepository
{
    Task<GetIndicesResultDto> GetIndices(GetIndicesRequest request, CancellationToken cancellationToken);
}