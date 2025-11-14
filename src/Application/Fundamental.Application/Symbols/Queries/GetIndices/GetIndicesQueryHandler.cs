using Fundamental.Application.Symbols.Repositories;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetIndices;

public sealed class GetIndicesQueryHandler(IRepository genericRepository, IIndicesRepository repository)
    : IRequestHandler<GetIndicesRequest, Response<GetIndicesResultDto>>
{
    public async Task<Response<GetIndicesResultDto>> Handle(GetIndicesRequest request, CancellationToken cancellationToken)
    {
        if (!await genericRepository.AnyAsync(new SymbolSpec().WhereIsin(request.Isin), cancellationToken))
        {
            return GetIndicesErrorCodes.IndexNotFound;
        }

        return await repository.GetIndices(request, cancellationToken);
    }
}