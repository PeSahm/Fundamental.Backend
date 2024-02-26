using System.Net;
using System.Net.Http.Json;
using System.Text;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Application.Common.Extensions;
using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Common;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Fundamental.Infrastructure.Services.Codals;

public class CodalService(
    IHttpClientFactory httpClientFactory,
    IOptions<MdpOption> monthlyActivityOption,
    ILogger<CodalService> logger,
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
                cancellationToken: cancellationToken);
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        using FundamentalDbContext context = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        if (response?.Result is { Count: > 0 })
        {
            foreach (GetStatementResponse statementResponse in response.Result)
            {
                string? isin = await context.Publishers.AsNoTracking()
                    .Where(x => x.CodalId == statementResponse.PublisherId.ToString())
                    .Select(x => x.Symbol.Isin)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

                if (!string.IsNullOrWhiteSpace(isin))
                {
                    statementResponse.Isin = isin;
                }
            }
        }

        return response?.Result ?? new();
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
                cancellationToken: cancellationToken);
        using IServiceScope scope = serviceScopeFactory.CreateScope();
        using FundamentalDbContext context = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        if (response?.Result is { Count: > 0 })
        {
            foreach (GetStatementResponse statementResponse in response.Result)
            {
                string? isin = await context.Publishers.AsNoTracking()
                    .Where(x => x.CodalId == statementResponse.PublisherId.ToString())
                    .Select(x => x.Symbol.Isin)
                    .FirstOrDefaultAsync(cancellationToken: cancellationToken);

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
        HttpResponseMessage response =
            await _mdpClient.GetAsync(
                requestUri: new StringBuilder()
                    .Append(_mdpOption.StatementJson)
                    .Append('/')
                    .Append(statement.TracingNo).ToString(),
                cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            logger.LogWarning(message: "Failed to get statement json for trace no {@TraceNo}", args: statement.TracingNo);
            return;
        }

        if (response.StatusCode == HttpStatusCode.NoContent)
        {
            return;
        }

        GetStatementJsonResponse? jsonData =
            await response.Content.ReadFromJsonAsync<GetStatementJsonResponse>(cancellationToken: cancellationToken);

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

        if (string.IsNullOrEmpty(statement.Isin))
        {
            return;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        ICodalProcessorFactory codalProcessorFactory = scope.ServiceProvider.GetRequiredService<ICodalProcessorFactory>();

        ICodalProcessor processor =
            codalProcessorFactory.GetCodalProcessor(jsonData.Json, statement.ReportingType, statement.Type, letterPart);

        await processor.Process(statement, jsonData, cancellationToken);
    }

    public async Task<List<GetPublisherResponse>> GetPublishers(CancellationToken cancellationToken = default)
    {
        HttpResponseMessage publisherResponse = await _mdpClient.GetAsync(
            new StringBuilder()
                .Append(_mdpOption.Publishers)
                .ToString(),
            cancellationToken: cancellationToken);

        if (!publisherResponse.IsSuccessStatusCode)
        {
            logger.LogError(message: "Failed to get publishers");
            return new();
        }

        string stringResponse =
            await publisherResponse.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

        if (string.IsNullOrWhiteSpace(stringResponse))
        {
            logger.LogError(message: "Failed to get publishers");
            return new();
        }

        List<GetPublisherResponse>? publishers = JsonConvert.DeserializeObject<List<GetPublisherResponse>>(stringResponse);
        return publishers ?? new();
    }
}