using System.Net.Http.Json;
using System.Text;
using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Common;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fundamental.Infrastructure.Services;

public class MarketDataService(
    IHttpClientFactory httpClientFactory,
    IOptions<MdpOption> mdpOption,
    IOptions<TseTmcOption> tseTmcOption
)
    : IMarketDataService
{
    private readonly HttpClient _mdpClient = httpClientFactory.CreateClient(HttpClients.MDP);
    private readonly MdpOption _mdpOption = mdpOption.Value;
    private readonly HttpClient _tseTmcClient = httpClientFactory.CreateClient(HttpClients.TSE_TMC);
    private readonly TseTmcOption _tseTmcOption = tseTmcOption.Value;

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
            cancellationToken);

        return response ?? [];
    }

    public async Task<List<TradeHistoryResponse>> GetClosePricesAsync(DateOnly date, CancellationToken cancellationToken = default)
    {
        List<TradeHistoryResponse>? response = await _mdpClient.GetFromJsonAsync<List<TradeHistoryResponse>>(
            new StringBuilder()
                .Append(_mdpOption.TradeHistory)
                .Append('?')
                .Append("select=Isin,TradeDate,OpenPrice,HighPrice,LowPrice,ClosePrice,LastPrice,OpenPriceAdj," +
                        "HighPriceAdj,LowPriceAdj,ClosePriceAdj,LastPriceAdj,TradeVolume,TradeValue,TradeQuantity," +
                        "ClosingPriceChange,ClosingPriceChangePercent,PrevClosePrice,PrevClosePriceAdj")
                .Append('&')
                .Append("TradeDate")
                .Append('=')
                .Append($"{date:yyyy-MM-dd}")
                .ToString(),
            cancellationToken);

        return response ?? [];
    }

    public async Task<List<SymbolResponse>> GetSymbolsAsync(CancellationToken cancellationToken = default)
    {
        string response = await _mdpClient.GetStringAsync(
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
                    "SymbolCustomExtension.ProductType" +
                    "SymbolCustomExtension.CustomExchangeType" +
                    "SymbolCustomExtension.EtfType"
                )
                .Append('&')
                .Append("status")
                .Append('=')
                .Append('1')
                .Append('&')
                .Append("MarketCode")
                .Append('=')
                .Append("ID,NO")
                .ToString(),
            cancellationToken);

        return JsonConvert.DeserializeObject<List<SymbolResponse>>(response) ?? [];
    }

    public async Task<IndexResponse> GetIndicesAsync(DateOnly fromDate, CancellationToken cancellationToken = default)
    {
        string url = new StringBuilder()
            .Append(_mdpOption.Index)
            .Append('?')
            .Append("FromDate=")
            .Append(fromDate.ToString("yyyy-MM-dd"))
            .Append('&')
            .Append("Page=1")
            .Append('&')
            .Append("Take=1000000")
            .ToString();

        HttpResponseMessage response = await _mdpClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<IndexResponse>(await response.Content.ReadAsStringAsync(cancellationToken)) ??
               new IndexResponse();
    }

    public async Task<IndexCompanyResponse> GetIndexCompanies(Symbol index, CancellationToken cancellationToken = default)
    {
        string url = new StringBuilder()
            .Append(_tseTmcOption.IndexCompany)
            .Append('/')
            .Append(index.TseInsCode)
            .ToString();
        HttpResponseMessage response = await _tseTmcClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<IndexCompanyResponse>(await response.Content.ReadAsStringAsync(cancellationToken)) ??
               new IndexCompanyResponse();
    }

    public async Task<ClosingPriceInfoResponse> GetClosingPriceInfo(string tseInsCode, CancellationToken cancellationToken = default)
    {
        string url = new StringBuilder()
            .Append(_tseTmcOption.ClosingPriceInfo)
            .Append(tseInsCode)
            .ToString();

        HttpResponseMessage response = await _tseTmcClient.GetAsync(url, cancellationToken);
        response.EnsureSuccessStatusCode();
        return JsonConvert.DeserializeObject<ClosingPriceInfoResponse>(
            await response.Content.ReadAsStringAsync(cancellationToken)) ?? new ClosingPriceInfoResponse();
    }
}