using Fundamental.Application.Symbols.Queries;
using Fundamental.Domain.Common.Dto;

namespace Fundamental.Application.Symbols.Repositories;

public interface ISymbolRelationRepository
{
    Task<Paginated<GetSymbolRelationsResultItem>> GetSymbolRelations(
        GetSymbolRelationsRequest request,
        CancellationToken cancellationToken
    );
}