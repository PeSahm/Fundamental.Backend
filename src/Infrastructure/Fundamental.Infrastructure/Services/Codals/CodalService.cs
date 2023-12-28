using System.Net.Http.Json;
using System.Text;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Options;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models;
using Fundamental.Domain.Common.Enums;
using Fundamental.Infrastructure.Common;
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

        return response?.Result ?? new();
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
            logger.LogError(message: "Failed to get statement json for trace no {@TraceNo}", args: statement.TracingNo);
            return;
        }

        GetStatementJsonResponse? jsonData =
            await response.Content.ReadFromJsonAsync<GetStatementJsonResponse>(cancellationToken: cancellationToken);

        if (string.IsNullOrWhiteSpace(jsonData?.Json))
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