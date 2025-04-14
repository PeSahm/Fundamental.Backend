using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsSales;

public sealed class UpdateFsSalesRequestCommandHandler(
    IFinancialStatementReadRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateFsSalesRequestCommandHandler> logger,
    ResiliencePipelineProvider<string> pipelineProvider
) : IRequestHandler<UpdateFsSalesRequest, Response>
{
    public async Task<Response> Handle(UpdateFsSalesRequest request, CancellationToken cancellationToken)
    {
        FinancialStatement? fs = await repository.GetLastFinancialStatement(request.Event.Isin, request.Event.FiscalYear, request.Event.ReportMonth, cancellationToken);
        ResiliencePipeline pipeline = pipelineProvider.GetPipeline("DbUpdateConcurrencyException");

        if (fs is null)
        {
            logger.LogWarning(
                "Financial statement not found for ISIN: {Isin} on report month: {ReportMonth}",
                request.Event.Isin,
                request.Event.ReportMonth);
            return UpdateFsSalesErrorCodes.StatementNotFound;
        }

        fs.SetSale(
            request.Event.SaleCurrentMonth,
            request.Event.ReportMonth,
            request.Event.SaleBeforeCurrentMonth,
            request.Event.SaleLastYear);

        await pipeline.ExecuteAsync(
            async _ =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            },
            cancellationToken);
        return Response.Successful();
    }
}