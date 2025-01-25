using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

public sealed class GetSymbolsQueryHandler : IRequestHandler<GetSymbolsRequest, Response<List<GetSymbolsResultDto>>>
{
    private readonly IRepository<Symbol> _symbolRepository;

    public GetSymbolsQueryHandler(IRepository<Symbol> symbolRepository)
    {
        _symbolRepository = symbolRepository;
    }

    public async Task<Response<List<GetSymbolsResultDto>>> Handle(GetSymbolsRequest request, CancellationToken cancellationToken)
    {
        List<GetSymbolsResultDto> symbols = await _symbolRepository.ListAsync(
            new SymbolSpec()
                .Filter(request.Filter)
                .ShowOfficialSymbols(request.ShowOfficialSymbolsOnly)
                .Select(),
            cancellationToken);

        return symbols;
    }
}