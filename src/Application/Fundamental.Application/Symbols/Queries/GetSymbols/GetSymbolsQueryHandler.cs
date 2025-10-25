using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSymbols;

public sealed class GetSymbolsQueryHandler(IRepository repository, IMarketDataService marketDataService)
    : IRequestHandler<GetSymbolsRequest, Response<List<GetSymbolsResultDto>>>
{
    public async Task<Response<List<GetSymbolsResultDto>>> Handle(GetSymbolsRequest request, CancellationToken cancellationToken)
    {
        List<GetSymbolsResultDto> symbols = await repository.ListAsync(
            new SymbolSpec()
                .Filter(request.Filter.Safe())
                .FilterReportingTypes(request.ReportingTypes)
                .ShowOfficialSymbols(request.ShowOfficialSymbolsOnly)
                .Select(),
            cancellationToken);
        symbols.RemoveAll(x => Guid.TryParse(x.TseInsCode, out _));

        foreach (GetSymbolsResultDto symbol in symbols)
        {
            ClosingPriceInfoResponse priceData = await marketDataService.GetCachedClosingPriceInfo(symbol.TseInsCode, cancellationToken);

            if (priceData.ClosingPriceInfo == null)
            {
                continue;
            }

            symbol.PriceInfo =
                new SymbolPriceInfo(
                    priceData.ClosingPriceInfo.PDrCotVal,
                    priceData.ClosingPriceInfo.PClosing,
                    priceData.ClosingPriceInfo.PriceYesterday
                );
        }

        return symbols;
    }
}