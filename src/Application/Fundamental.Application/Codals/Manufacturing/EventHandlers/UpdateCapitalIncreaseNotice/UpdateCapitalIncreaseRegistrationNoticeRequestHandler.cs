using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.Manufacturing.Enums;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.Constants;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateCapitalIncreaseNotice;

public sealed class UpdateCapitalIncreaseRegistrationNoticeRequestHandler(
    ILogger<UpdateCapitalIncreaseRegistrationNoticeRequestHandler> logger,
    IIncomeStatementsReadRepository incomeStatementsReadRepository,
    IFinancialStatementReadRepository financialStatementReadRepository,
    IRepository repository,
    IUnitOfWork unitOfWork,
    ResiliencePipelineProvider<string> pipelineProvider
)
    : IRequestHandler<UpdateCapitalIncreaseRegistrationNoticeRequest, Response>
{
    public async Task<Response> Handle(UpdateCapitalIncreaseRegistrationNoticeRequest request, CancellationToken cancellationToken)
    {
        ResiliencePipeline pipeline = pipelineProvider.GetPipeline("DbUpdateConcurrencyException");
        (FiscalYear Year, StatementMonth Month, ulong TraceNo) latestStatementData =
            await incomeStatementsReadRepository.GetLatestStatement(request.Event.Isin, cancellationToken);
        IncomeStatement? capitalRow = await repository.FirstOrDefaultAsync(
            new IncomeStatementSpec()
                .WhereIsin(request.Event.Isin)
                .WithTraceNo(latestStatementData.TraceNo)
                .WhereFiscalYear(latestStatementData.Year)
                .WhereReportMonth(latestStatementData.Month)
                .WhereIncomeStatementRow(IncomeStatementRow.ListedCapital),
            cancellationToken);

        if (capitalRow == null)
        {
            logger.LogWarning(
                "Capital row not found for ISIN {Isin} with TraceNo {TraceNo}, FiscalYear {FiscalYear}, ReportMonth {ReportMonth}",
                request.Event.Isin,
                latestStatementData.TraceNo,
                latestStatementData.Year,
                latestStatementData.Month
            );
            return UpdateIncomeStatementCapitalErrorCodes.CapitalRecordNotFound;
        }

        capitalRow.UpdateCapitalValue(request.Event.NewCapital / SignedCodalMoney.CodalMoneyMultiplier);

        FinancialStatement? fs = await financialStatementReadRepository.GetLastFinancialStatement(
            request.Event.Isin,
            latestStatementData.Year,
            latestStatementData.Month,
            cancellationToken);

        if (fs == null)
        {
            logger.LogWarning(
                "Financial statement not found for ISIN {Isin} with TraceNo {TraceNo}, FiscalYear {FiscalYear}, ReportMonth {ReportMonth}",
                request.Event.Isin,
                latestStatementData.TraceNo,
                latestStatementData.Year,
                latestStatementData.Month
            );
            return UpdateIncomeStatementCapitalErrorCodes.FsRecordNotFound;
        }

        fs.SetMarketCap(
            (request.Event.NewCapital / SignedCodalMoney.CodalMoneyMultiplier) / IranCapitalMarket.BASE_PRICE);

        await pipeline.ExecuteAsync(
            async _ =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            },
            cancellationToken);
        return Response.Successful();
    }
}