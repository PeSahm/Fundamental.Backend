using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Domain.Symbols.Enums;

namespace Fundamental.Application.Symbols;

public interface IShareHoldersService
{
    Task CreateShareHolders(
        List<ShareHoldersResponse> shareHolders,
        ShareHolderSource shareHolderSource,
        CancellationToken cancellationToken = default
    );
}