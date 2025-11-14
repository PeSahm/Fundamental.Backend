using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Jobs.UpdateTseTmcShareHoldersData;

public sealed class UpdateTseTmcShareHoldersDataCommandHandler(
    IMarketDataService marketDataService,
    IShareHoldersService shareHoldersService
) : IRequestHandler<UpdateTseTmcShareHoldersDataRequest, Response>
{
    public async Task<Response> Handle(UpdateTseTmcShareHoldersDataRequest request, CancellationToken cancellationToken)
    {
        List<ShareHoldersResponse> shareHolders =
            await marketDataService.GetShareHoldersAsync(DateOnly.FromDateTime(DateTime.Now.AddDays(-1)), cancellationToken);

        await shareHoldersService.CreateShareHolders(shareHolders, cancellationToken);

        return Response.Successful();
    }
}