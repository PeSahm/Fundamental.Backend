using System.Net.Http.Json;
using Fundamental.Application.Codal.Dto.MonthlyActivities.V4;
using Fundamental.Application.Codal.Dto.MonthlyActivities.V4.Enums;
using Fundamental.Application.Codal.Options;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.Infrastructure.Common;
using Fundamental.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using MonthlyActivity = Fundamental.Domain.Statements.Entities.MonthlyActivity;

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

    public async Task<List<GetStatementResponse>> GetMonthlyActivities(
        DateTime fromDate,
        ReportingType reportingType,
        CancellationToken cancellationToken = default
    )
    {
        MdpPagedResponse<GetStatementResponse>? response =
            await _mdpClient.GetFromJsonAsync<MdpPagedResponse<GetStatementResponse>>(
                $"{_mdpOption.Statement}?FromDate={fromDate:yyyy-MM-dd}&ReportingType={(int)reportingType}&take=1000000&page=1&Types=58",
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

        await HandleMonthlyActivityV4(statement, model, cancellationToken);
    }

    private async Task HandleMonthlyActivityV4(
        GetStatementResponse statement,
        GetStatementJsonResponse model,
        CancellationToken cancellationToken
    )
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;
        CodalMonthlyActivity? saleDate =
            JsonConvert.DeserializeObject<CodalMonthlyActivity>(value: model.Json, settings: setting);

        if (saleDate is null)
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        await using FundamentalDbContext dbContext = scope.ServiceProvider.GetRequiredService<FundamentalDbContext>();

        if (saleDate.MonthlyActivity.ProductionAndSales.YearData.Count == 0)
        {
            return;
        }

        YearDatum? yearDatum = saleDate.MonthlyActivity.ProductionAndSales.YearData
            .FirstOrDefault(x => x.ColumnId == SaleColumnId.SaleThisMonth);

        if (yearDatum is null)
        {
            return;
        }

        if (yearDatum.FiscalYear is null || yearDatum.FiscalMonth is null || yearDatum.ReportMonth is null)
        {
            return;
        }

        MonthlyActivity? existingStatement = await dbContext.MonthlyActivities
            .FirstOrDefaultAsync(
                x =>
                    x.Symbol.Isin == statement.Isin && x.FiscalYear.Year == yearDatum.FiscalYear &&
                    x.ReportMonth.Month == yearDatum.ReportMonth,
                cancellationToken: cancellationToken);
        Symbol symbol =
            await dbContext.Symbols.FirstAsync(
                predicate: x => x.Isin == statement.Isin,
                cancellationToken: cancellationToken);

        if (existingStatement == null)
        {
            MonthlyActivity monthlyActivity = new(
                id: Guid.NewGuid(),
                symbol: symbol,
                traceNo: statement.TracingNo,
                uri: statement.HtmlUrl,
                fiscalYear: yearDatum.FiscalYear,
                yearEndMonth: yearDatum.FiscalMonth.Value,
                reportMonth: yearDatum.ReportMonth.Value,
                saleBeforeCurrentMonth: GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountExclusiveThisMonth),
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountThisMonth),
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountInclusiveThisMonth),
                GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountPrevYear),
                false,
                DateTime.Now
            );
            dbContext.Add(monthlyActivity);
        }
        else
        {
            if (existingStatement.TraceNo < statement.TracingNo)
            {
                existingStatement.Update(
                    symbol: symbol,
                    traceNo: statement.TracingNo,
                    uri: statement.HtmlUrl,
                    fiscalYear: yearDatum.FiscalYear,
                    yearEndMonth: yearDatum.FiscalMonth.Value,
                    reportMonth: yearDatum.ReportMonth.Value,
                    saleBeforeCurrentMonth: GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountExclusiveThisMonth),
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountThisMonth),
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountInclusiveThisMonth),
                    GetSumRecord(saleDate).GetValue(SaleColumnId.SaleAmountPrevYear),
                    false,
                    DateTime.Now
                );
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private RowItem GetSumRecord(CodalMonthlyActivity saleDate) =>
        saleDate.MonthlyActivity.ProductionAndSales.RowItems.First(predicate: x =>
            x is { RowCode: RowCode.TotalSum, Category: Category.Sum });
}