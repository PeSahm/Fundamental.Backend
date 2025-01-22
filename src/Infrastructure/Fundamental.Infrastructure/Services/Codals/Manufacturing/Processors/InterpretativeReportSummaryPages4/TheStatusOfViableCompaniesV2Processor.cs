using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries.V2.
    TheStatusOfViableCompanies;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Application.Codals.Services.Models.MarketDataServiceModels;
using Fundamental.Application.Symbols;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Symbols.Enums;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages4;

public class TheStatusOfViableCompaniesV2Processor(IServiceScopeFactory serviceScopeFactory) : ICodalProcessor
{
    public static ReportingType ReportingType => ReportingType.Production;
    public static LetterType LetterType => LetterType.InterimStatement;
    public static CodalVersion CodalVersion => CodalVersion.V2;
    public static LetterPart LetterPart => LetterPart.TheStatusOfViableCompanies;

    public async Task Process(GetStatementResponse statement, GetStatementJsonResponse model, CancellationToken cancellationToken)
    {
        JsonSerializerSettings setting = new();
        setting.NullValueHandling = NullValueHandling.Ignore;
        JObject jObject = JObject.Parse(model.Json);

        // List of properties to keep
        HashSet<string> propertiesToKeep =
        [
            "listedCapital",
            "unauthorizedCapital",
            "interpretativeReportSummaryPage4"
        ];

        List<JProperty> properties = jObject.Properties().ToList();

        foreach (JProperty property in properties)
        {
            if (!propertiesToKeep.Contains(property.Name))
            {
                property.Remove();
            }
        }

        // Deserialize the filtered JSON into your model
        RootInterpretativeReportSummaryPage4? rootInterpretativeReportSummary = jObject.ToObject<RootInterpretativeReportSummaryPage4>(
            new JsonSerializer
            {
                NullValueHandling = NullValueHandling.Ignore
            });

        if (rootInterpretativeReportSummary?.InterpretativeReportSummaryPage4 is null)
        {
            return;
        }

        if (rootInterpretativeReportSummary.InterpretativeReportSummaryPage4.TheStatusOfViableCompanies is null)
        {
            return;
        }

        if (!rootInterpretativeReportSummary.InterpretativeReportSummaryPage4.TheStatusOfViableCompanies.IsValidReport())
        {
            return;
        }

        CodalStatusOfViableCompanies? theStatusOfViableCompanies =
            rootInterpretativeReportSummary.InterpretativeReportSummaryPage4.TheStatusOfViableCompanies;

        theStatusOfViableCompanies.AddCustomRowItems();

        using IServiceScope scope = serviceScopeFactory.CreateScope();
        IShareHoldersService shareHoldersService = scope.ServiceProvider.GetRequiredService<IShareHoldersService>();
        List<ShareHoldersResponse> shareHolderRequest = rootInterpretativeReportSummary.InterpretativeReportSummaryPage4
            .TheStatusOfViableCompanies.RowItems.Where(x => !string.IsNullOrWhiteSpace(x.GetDescription())).Select(x =>
                new ShareHoldersResponse
                {
                    Isin = statement.Isin!,
                    ShareHolderName = x.GetDescription()!,
                    Percent = x.GetValue(ColumnId.CurrentOwnershipPercentage),
                }).ToList();
        await shareHoldersService.CreateShareHolders(shareHolderRequest, ShareHolderSource.Codal, cancellationToken);
    }
}