using System.Net.Http.Json;
using System.Text;
using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Infrastructure.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fundamental.Infrastructure.Services;

public class MarketDataService(
    IHttpClientFactory httpClientFactory,
    IOptions<MdpOption> mdpOption
)
    : IMarketDataService
{
    private readonly HttpClient _mdpClient = httpClientFactory.CreateClient(HttpClients.MDP);
    private readonly MdpOption _mdpOption = mdpOption.Value;

    public async Task<List<ShareHoldersResponse>> GetShareHoldersAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        List<ShareHoldersResponse>? response = await _mdpClient.GetFromJsonAsync<List<ShareHoldersResponse>>(
            new StringBuilder()
                .Append(_mdpOption.ShareHolders)
                .Append('?')
                .Append("select=Isin,Date,ShareHolderName,NumberOfShares,PerOfShares,ChangeAmount")
                .Append('&')
                .Append("Date")
                .Append('=')
                .Append($"{date:yyyy-MM-dd}")
                .ToString(),
            cancellationToken: cancellationToken);

        return response ?? [];
    }

    public async Task<List<TradeHistoryResponse>> GetClosePricesAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        List<TradeHistoryResponse>? response = await _mdpClient.GetFromJsonAsync<List<TradeHistoryResponse>>(
            new StringBuilder()
                .Append(_mdpOption.TradeHistory)
                .Append('?')
                .Append("select=Isin,Date,ShareHolderName,NumberOfShares,PerOfShares,ChangeAmount")
                .Append('&')
                .Append("Date")
                .Append('=')
                .Append($"{date:yyyy-MM-dd}")
                .ToString(),
            cancellationToken: cancellationToken);

        return response ?? [];
    }

    public async Task<List<SymbolResponse>> GetSymbolsAsync(CancellationToken cancellationToken = default)
    {
        string? response = await _mdpClient.GetStringAsync(
            new StringBuilder()
                .Append(_mdpOption.Symbol)
                .Append('?')
                .Append("select=")
                .Append(
                    "Isin," +
                    "TseInsCode," +
                    "EnName," +
                    "SymbolEnName," +
                    "Title,Name," +
                    "CompanyEnCode," +
                    "CompanyPersianName," +
                    "CompanyIsin,MarketCap," +
                    "SectorCode," +
                    "SubSectorCode," +
                    "SymbolCustomExtension.ProductType")
                .Append('&')
                .Append("status")
                .Append('=')
                .Append('1')
                .Append('&')
                .Append("MarketCode")
                .Append('=')
                .Append("ID,NO")
                .ToString(),
            cancellationToken: cancellationToken);

        return JsonConvert.DeserializeObject<List<SymbolResponse>>(response) ?? [];
    }
}