using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Domain.Symbols.Entities;

namespace Fundamental.Application.Codals.Services;

public interface IMarketDataService
{
    Task<List<ShareHoldersResponse>> GetShareHoldersAsync(DateOnly date, CancellationToken cancellationToken = default);

    Task<List<TradeHistoryResponse>> GetClosePricesAsync(DateOnly date, CancellationToken cancellationToken = default);

    Task<List<SymbolResponse>> GetSymbolsAsync(CancellationToken cancellationToken = default);

    Task<IndexResponse> GetIndicesAsync(DateOnly fromDate, CancellationToken cancellationToken = default);
    Task<IndexCompanyResponse> GetIndexCompanies(Symbol index, CancellationToken cancellationToken = default);
    Task<ClosingPriceInfoResponse> GetClosingPriceInfo(string tseInsCode, CancellationToken cancellationToken = default);
    Task<ClosingPriceInfoResponse> GetCachedClosingPriceInfo(string tseInsCode, CancellationToken cancellationToken = default);
}