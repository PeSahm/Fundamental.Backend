using DNTPersianUtils.Core;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Prices.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Prices.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Prices.Jobs.UpdateClosePrices;

public sealed class UpdateClosePricesCommandHandler(
    IRepository<ClosePrice> closePriceRepository,
    IRepository<Symbol> symbolRepository,
    IMarketDataService marketDataService,
    IUnitOfWork unitOfWork
)
    : IRequestHandler<UpdateClosePricesDataCommand, Response>
{
    public async Task<Response> Handle(UpdateClosePricesDataCommand dataCommand, CancellationToken cancellationToken)
    {
        DateOnly fromDate = DateTime.Now.AddDays(-1 * dataCommand.Days).ToDateOnly();
        DateOnly toDate = DateTime.Now.AddDays(1).ToDateOnly();

        while (fromDate <= toDate)
        {
            List<TradeHistoryResponse> prices = await marketDataService.GetClosePricesAsync(fromDate, cancellationToken);

            if (prices.Any())
            {
                foreach (TradeHistoryResponse price in prices)
                {
                    bool symbolExists = await symbolRepository.AnyAsync(new SymbolSpec().WhereIsin(price.Isin), cancellationToken);

                    if (!symbolExists)
                    {
                        continue;
                    }

                    Symbol symbol =
                        (await symbolRepository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(price.Isin), cancellationToken))!;

                    ClosePrice? closePrice =
                        await closePriceRepository.FirstOrDefaultAsync(ClosePriceSpec.Where(price.Isin, fromDate), cancellationToken);

                    if (closePrice is null)
                    {
                        closePrice = new ClosePrice(Guid.NewGuid(), symbol, fromDate, DateTime.Now);
                        AddPriceDetails(closePrice, price);
                        closePriceRepository.Add(closePrice);
                    }
                    else
                    {
                        AddPriceDetails(closePrice, price);
                    }
                }

                await unitOfWork.SaveChangesAsync(cancellationToken);
            }

            fromDate = fromDate.AddDays(1);
        }

        return Response.Successful();
    }

    private static void AddPriceDetails(ClosePrice closePrice, TradeHistoryResponse price)
    {
        closePrice.SetPrice(
            (ulong)price.ClosePrice,
            (ulong)price.OpenPrice,
            (ulong)price.HighPrice,
            (ulong)price.LowPrice,
            (ulong)price.LastPrice,
            DateTime.Now
        );

        closePrice.SetAdjustedPrice(
            (ulong)price.ClosePriceAdj,
            (ulong)price.OpenPriceAdj,
            (ulong)price.HighPriceAdj,
            (ulong)price.LowPriceAdj,
            (ulong)price.LastPriceAdj,
            DateTime.Now
        );

        closePrice.SetPriceStatistics(
            (ulong)price.TradeVolume,
            (ulong)price.TradeQuantity,
            (ulong)price.TradeValue,
            DateTime.Now
        );
    }
}