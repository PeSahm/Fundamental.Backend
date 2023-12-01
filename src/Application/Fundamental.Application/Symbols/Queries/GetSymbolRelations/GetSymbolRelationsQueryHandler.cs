using Fundamental.Application.Symbols.Repositories;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelations;

public sealed class
    GetSymbolRelationsQueryHandler : IRequestHandler<GetSymbolRelationsRequest, Response<Paginated<GetSymbolRelationsResultItem>>>
{
    private readonly ISymbolRelationRepository _repository;

    public GetSymbolRelationsQueryHandler(ISymbolRelationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Response<Paginated<GetSymbolRelationsResultItem>>> Handle(
        GetSymbolRelationsRequest request,
        CancellationToken cancellationToken
    )
    {
        Paginated<GetSymbolRelationsResultItem> result = await _repository.GetSymbolRelations(request, cancellationToken);
        return result;
    }
}