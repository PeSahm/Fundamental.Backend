using System.Net;
using System.Net.Http.Json;
using System.Text;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Codals;
using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Caching;
using Fundamental.Infrastructure.Common;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;

namespace Fundamental.Infrastructure.Services.Codals;

public class CodalService(
    IHttpClientFactory httpClientFactory,
    IOptions<MdpOption> monthlyActivityOption,
    HybridCache cache,
    IServiceScopeFactory serviceScopeFactory
)
    : ICodalService
{
    private readonly HttpClient _mdpClient = httpClientFactory.CreateClient(HttpClients.MDP);
    private readonly MdpOption _mdpOption = monthlyActivityOption.Value;

    public async Task<List<GetStatementResponse>> GetStatements(
        DateTime fromDate,
        ReportingType reportingType,
        LetterType letterType,
        CancellationToken cancellationToken = default
    )
    {
        // Ensure fromDate is in UTC
        if (fromDate.Kind != DateTimeKind.Utc)
        {
            fromDate = DateTime.SpecifyKind(fromDate, DateTimeKind.Utc);
        }

        MdpPagedResponse<GetStatementResponse>? response =
            await _mdpClient.GetFromJsonAsync<MdpPagedResponse<GetStatementResponse>>(
                new StringBuilder()
                    .Append(_mdpOption.Statement)
                    .Append("?FromDate=")
                    .Append($"{fromDate:yyyy-MM-dd}")
                    .Append("&ReportingType=")
                    .Append((int)reportingType)
                    .Append("&take=1000000&page=1&Types=")
                    .Append((int)letterType)
                    .ToString(),
                cancellationToken);
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        using FundamentalDbContext context = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        if (response?.Result is { Count: > 0 })
        {
            List<(string CodalId, string Isin)> codalIdIsinPairs = await GetCodalIdIsinPair(cancellationToken, context);

            foreach (GetStatementResponse statementResponse in response.Result)
            {
                string? theSymbolIsin = codalIdIsinPairs
                    .Where(x => x.CodalId == statementResponse.PublisherId.ToString())
                    .Select(x => x.Isin)
                    .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(theSymbolIsin))
                {
                    statementResponse.Isin = theSymbolIsin;
                }
            }
        }

        return response?.Result ?? new List<GetStatementResponse>();
    }

    public async Task<GetStatementResponse?> GetStatementByTraceNo(ulong traceNo, CancellationToken cancellationToken = default)
    {
        MdpPagedResponse<GetStatementResponse>? response =
            await _mdpClient.GetFromJsonAsync<MdpPagedResponse<GetStatementResponse>>(
                new StringBuilder()
                    .Append(_mdpOption.Statement)
                    .Append("?TraceNo=")
                    .Append(traceNo)
                    .ToString(),
                cancellationToken);
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        using FundamentalDbContext context = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        if (response?.Result is { Count: > 0 })
        {
            List<(string CodalId, string Isin)> codalIdIsinPairs = await GetCodalIdIsinPair(cancellationToken, context);

            foreach (GetStatementResponse statementResponse in response.Result)
            {
                string? isin = codalIdIsinPairs
                    .Where(x => x.CodalId == statementResponse.PublisherId.ToString())
                    .Select(x => x.Isin)
                    .FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(isin))
                {
                    statementResponse.Isin = isin;
                }
            }
        }

        return response?.Result?.FirstOrDefault();
    }

    public async Task ProcessCodal(GetStatementResponse statement, LetterPart letterPart, CancellationToken cancellationToken = default)
    {
        await ProcessCodal(statement, statement.ReportingType, letterPart, cancellationToken);
    }

    public async Task ProcessCodal(
        GetStatementResponse statement,
        ReportingType reportingType,
        LetterPart letterPart,
        CancellationToken cancellationToken = default
    )
    {
        if (string.IsNullOrEmpty(statement.Isin))
        {
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        // Try to get cached JSON from database first
        RawCodalJson? cachedJson = await dbContext.RawCodalJsons
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TraceNo == statement.TracingNo, cancellationToken);

        string? rawJson;
        GetStatementJsonResponse? jsonData;

        if (cachedJson is not null)
        {
            // Use cached JSON
            Log.Debug("Using cached JSON for TraceNo: {TraceNo}", statement.TracingNo);
            rawJson = cachedJson.RawJson;
            jsonData = new GetStatementJsonResponse { Json = rawJson };
        }
        else
        {
            // Fetch from API
            HttpResponseMessage response =
                await _mdpClient.GetAsync(
                    new StringBuilder()
                        .Append(_mdpOption.StatementJson)
                        .Append('/')
                        .Append(statement.TracingNo).ToString(),
                    cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                return;
            }

            if (response.StatusCode == HttpStatusCode.NoContent)
            {
                return;
            }

            rawJson = await response.Content.ReadAsStringAsync(cancellationToken);

            if (!rawJson.IsValidJson())
            {
                return;
            }

            jsonData = JsonConvert.DeserializeObject<GetStatementJsonResponse>(rawJson);

            if (jsonData is null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(jsonData.Json))
            {
                return;
            }

            if (!jsonData.Json.IsValidJson())
            {
                return;
            }

            // Save to database for future use
            await SaveRawCodalJsonAsync(dbContext, statement, jsonData.Json, cancellationToken);
        }

        ICodalProcessorFactory codalProcessorFactory = scope.ServiceProvider.GetRequiredService<ICodalProcessorFactory>();

        ICodalProcessor processor =
            codalProcessorFactory.GetCodalProcessor(jsonData.Json, reportingType, statement.Type, letterPart);

        await processor.Process(statement, jsonData, cancellationToken);
    }

    public async Task<List<GetPublisherResponse>> GetPublishers(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage publisherResponse = await _mdpClient.GetAsync(
            new StringBuilder()
                .Append(_mdpOption.Publishers)
                .ToString(),
            cancellationToken);

        if (!publisherResponse.IsSuccessStatusCode)
        {
            return new List<GetPublisherResponse>();
        }

        string stringResponse =
            await publisherResponse.Content.ReadAsStringAsync(cancellationToken);

        if (string.IsNullOrWhiteSpace(stringResponse))
        {
            return new List<GetPublisherResponse>();
        }

        List<GetPublisherResponse>? publishers = JsonConvert.DeserializeObject<List<GetPublisherResponse>>(stringResponse);
        return publishers ?? new List<GetPublisherResponse>();
    }

    private static async Task SaveRawCodalJsonAsync(
        FundamentalDbContext dbContext,
        GetStatementResponse statement,
        string json,
        CancellationToken cancellationToken)
    {
        Uri? htmlUrl = null;
        if (!string.IsNullOrEmpty(statement.HtmlUrl))
        {
            try
            {
                htmlUrl = new Uri(statement.HtmlUrl);
            }
            catch (UriFormatException)
            {
                // Invalid URL, leave as null
            }
        }

        RawCodalJson rawCodalJson = new(
            id: Guid.NewGuid(),
            traceNo: statement.TracingNo,
            publishDate: statement.PublishDateMiladi.ToUniversalTime(),
            reportingType: statement.ReportingType,
            statementLetterType: statement.Type,
            htmlUrl: htmlUrl,
            publisherId: statement.PublisherId,
            isin: statement.Isin,
            rawJson: json,
            createdAt: DateTime.UtcNow);

        dbContext.RawCodalJsons.Add(rawCodalJson);
        await dbContext.SaveChangesAsync(cancellationToken);

        Log.Debug("Saved raw JSON to database for TraceNo: {TraceNo}", statement.TracingNo);
    }

    private async Task<List<(string CodalId, string Isin)>> GetCodalIdIsinPair(
        CancellationToken cancellationToken,
        FundamentalDbContext context
    )
    {
        return await cache.GetOrCreateAsync(
            CacheKeys.MonthlyActivity.CodalIdIsinPair(),
            async (cancel) =>
            {
                return await context.Publishers.AsNoTracking()
                    .Select(x => new ValueTuple<string, string>(x.CodalId, x.Symbol.Isin))
                    .ToListAsync(cancel);
            },
            new HybridCacheEntryOptions
            {
                LocalCacheExpiration = TimeSpan.FromHours(4),
                Expiration = TimeSpan.FromDays(5),
            },
            cancellationToken: cancellationToken
        );
    }
}