using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

namespace Fundamental.Application.Codals.Services;

public interface IMarketDataService
{
    Task<List<ShareHoldersResponse>> GetShareHoldersAsync(DateOnly date, CancellationToken cancellationToken = default);
}