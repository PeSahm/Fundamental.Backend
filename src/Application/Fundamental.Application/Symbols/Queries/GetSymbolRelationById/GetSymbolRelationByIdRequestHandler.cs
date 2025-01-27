using Fundamental.Application.Symbols.Queries.GetSymbolRelations;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbolRelationById;

public sealed class
    GetSymbolRelationByIdRequestHandler(IRepository repository)
    : IRequestHandler<GetSymbolRelationByIdRequest, Response<GetSymbolRelationsResultItem>>
{
    public async Task<Response<GetSymbolRelationsResultItem>> Handle(
        GetSymbolRelationByIdRequest request,
        CancellationToken cancellationToken
    )
    {
        GetSymbolRelationsResultItem? result = await repository.FirstOrDefaultAsync(
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