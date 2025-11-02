using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries;
using Fundamental.Application.Codals.Dto.FinancialStatements.ManufacturingCompanies.InterpretativeReportPage4Summaries.V2.
    TheStatusOfViableCompanies;
using Fundamental.Application.Codals.Enums;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Fundamental.Infrastructure.Services.Codals.Manufacturing.Processors.InterpretativeReportSummaryPages4;

public class TheStatusOfViableCompaniesV2Processor(
    IServiceScopeFactory serviceScopeFactory,
    ILogger<TheStatusOfViableCompaniesV2Processor> logger
) : ICodalProcessor
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
        IUnitOfWork unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        IRepository repository = scope.ServiceProvider.GetRequiredService<IRepository>();

        Symbol? parentSymbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(statement.Isin!), cancellationToken);

        if (parentSymbol is null)
        {
            logger.LogError("Parent symbol with ISIN {Isin} not found for Statement {@Statement}", statement.Isin, statement);
            return;
        }

        foreach (RowItem childCompany in rootInterpretativeReportSummary.InterpretativeReportSummaryPage4.TheStatusOfViableCompanies
                     .RowItems.Where(x => !string.IsNullOrWhiteSpace(x.GetDescription())))
        {
            StockOwnership? existsOwnerShip = await repository.FirstOrDefaultAsync(
                new StockOwnershipSpec()
                    .WhereParentSymbolId(parentSymbol.Id)
                    .WhereSubsidiarySymbolName(childCompany.GetDescription()!.Safe()!)
                    .WhereTraceNo(statement.TracingNo),
                cancellationToken);

            if (existsOwnerShip is not null)
            {
                existsOwnerShip.ChangeOwnershipPercentage(
                    childCompany.GetValue(ColumnId.CurrentOwnershipPercentage),
                    childCompany.GetValue(ColumnId.CurrentCostBasis),
                    statement.TracingNo,
                    statement.HtmlUrl,
                    DateTime.Now
                );
            }
            else
            {
                StockOwnership stockOwnership = new(
                    Guid.NewGuid(),
                    parentSymbol,
                    childCompany.GetDescription()!.Safe()!,
                    childCompany.GetValue(ColumnId.CurrentOwnershipPercentage),
                    childCompany.GetValue(ColumnId.CurrentCostBasis),
                    statement.TracingNo,
                    statement.HtmlUrl,
                    DateTime.Now
                );
                repository.Add(stockOwnership);
            }
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}