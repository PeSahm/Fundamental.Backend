using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Symbols.Queries.GetSymbols;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetFinancialStatements;

public sealed class GetFinancialStatementsQueryHandler(IRepository repository, IMarketDataService marketDataService)
    : IRequestHandler<GetFinancialStatementsReqeust, Response<GetFinancialStatementsResultDto>>
{
    public async Task<Response<GetFinancialStatementsResultDto>> Handle(
        GetFinancialStatementsReqeust request,
        CancellationToken cancellationToken
    )
    {
        GetFinancialStatementsResultDto? result =
            await repository.FirstOrDefaultAsync(
                new FinancialStatementsSpec()
                    .WhereIsin(request.Isin)
                    .ToResultDto()
                    .OrderByLastRecord(),
                cancellationToken);

        if (result is null)
        {
            return GetFinancialStatementsErrorCodes.RecodeNotFound;
        }

        ClosingPriceInfoResponse priceData = await marketDataService.GetCachedClosingPriceInfo(result.TseInsCode, cancellationToken);
        result.PriceInfo =
            new SymbolPriceInfo(
                priceData.ClosingPriceInfo.PDrCotVal,
                priceData.ClosingPriceInfo.PClosing,
                priceData.ClosingPriceInfo.PriceYesterday
            );
        return result;
    }
}