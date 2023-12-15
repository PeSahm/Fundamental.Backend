using System.Net.Http.Json;
using System.Text;
using Fundamental.Application.Codal.Options;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;
using Fundamental.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Fundamental.Infrastructure.Services.Codal;

public class CodalService : ICodalService
{
    private readonly ILogger _logger;
    private readonly HttpClient _mdpClient;
    private readonly MdpOption _mdpOption;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CodalService(
        IHttpClientFactory httpClientFactory,
        IOptions<MdpOption> monthlyActivityOption,
        ILogger<CodalService> logger,
        IServiceScopeFactory serviceScopeFactory
    )
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _mdpOption = monthlyActivityOption.Value;
        _mdpClient = httpClientFactory.CreateClient(HttpClients.MDP);
    }

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

    public async Task UpsertMonthlyActivities(GetStatementResponse statement, CancellationToken cancellationToken = default)
    {
        HttpResponseMessage response =
            await _mdpClient.GetAsync(
                requestUri: $"{_mdpOption.StatementJson}/{statement.TracingNo}",
                cancellationToken: cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogError(message: "Failed to get statement json for trace no {@TraceNo}", args: statement.TracingNo);
            return;
        }

        GetStatementJsonResponse? model =
            await response.Content.ReadFromJsonAsync<GetStatementJsonResponse>(cancellationToken: cancellationToken);

        if (model is null)
        {
            _logger.LogError(message: "Failed to deserialize statement json for trace no {@TraceNo}", args: statement.TracingNo);
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        ICodalProcessorFactory codalProcessorFactory = scope.ServiceProvider.GetRequiredService<ICodalProcessorFactory>();

        ICodalProcessor processor =
            codalProcessorFactory.GetCodalProcessor(model.Json, statement.ReportingType, LetterType.MonthlyActivity);

        await processor.Process(statement, model, cancellationToken);
    }
}