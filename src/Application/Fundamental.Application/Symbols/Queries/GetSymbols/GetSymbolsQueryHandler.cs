using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

public sealed class GetSymbolsQueryHandler(IRepository repository)
    : IRequestHandler<GetSymbolsRequest, Response<List<GetSymbolsResultDto>>>
{
    public async Task<Response<List<GetSymbolsResultDto>>> Handle(GetSymbolsRequest request, CancellationToken cancellationToken)
    {
        List<GetSymbolsResultDto> symbols = await repository.ListAsync(
            new SymbolSpec()
                .Filter(request.Filter.Safe()!)
                .ShowOfficialSymbols(request.ShowOfficialSymbolsOnly)
                .Select(),
            cancellationToken);

        return symbols;
    }
}