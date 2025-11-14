using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;

namespace Fundamental.Application.Symbols;

public interface IShareHoldersService
{
    Task CreateShareHolders(
        List<ShareHoldersResponse> shareHolders,
        CancellationToken cancellationToken = default
    );
}