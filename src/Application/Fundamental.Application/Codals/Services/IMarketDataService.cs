using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

namespace Fundamental.Application.Codals.Services;

public interface IMarketDataService
{
    Task<List<ShareHoldersResponse>> GetShareHoldersAsync(DateOnly date, CancellationToken cancellationToken = default);

    Task<List<TradeHistoryResponse>> GetClosePricesAsync(DateOnly date, CancellationToken cancellationToken = default);

    Task<List<SymbolResponse>> GetSymbolsAsync(CancellationToken cancellationToken = default);
}