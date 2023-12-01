using Fundamental.Application.Symbols.Queries.GetSymbolRelations;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelationById;

public sealed class
    GetSymbolRelationByIdRequestHandler : IRequestHandler<GetSymbolRelationByIdRequest, Response<GetSymbolRelationsResultItem>>
{
    private readonly IRepository<SymbolRelation> _symbolRelationRepository;

    public GetSymbolRelationByIdRequestHandler(IRepository<SymbolRelation> symbolRelationRepository)
    {
        _symbolRelationRepository = symbolRelationRepository;
    }

    public async Task<Response<GetSymbolRelationsResultItem>> Handle(
        GetSymbolRelationByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetSymbolRelationsResultItem? result = await _symbolRelationRepository.FirstOrDefaultAsync(
            new SymbolRelationSpec()
                .WhereId(request.Id)
                .Select(),
            cancellationToken
        );

        if (result is null)
        {
            return GetSymbolRelationByIdErrorCodes.SymbolRelationNotFound;
        }

        return result;
    }
}