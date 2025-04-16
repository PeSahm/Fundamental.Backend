using Fundamental.Application.Codals.Manufacturing.Repositories;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Registry;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsClosePrice;

public sealed class UpdateFsClosePriceRequestCommandHandler(
    IFinancialStatementReadRepository repository,
    IUnitOfWork unitOfWork,
    ILogger<UpdateFsClosePriceRequestCommandHandler> logger,
    ResiliencePipelineProvider<string> pipelineProvider

)
    : IRequestHandler<UpdateFsClosePriceRequest, Response>
{
    public async Task<Response> Handle(UpdateFsClosePriceRequest request, CancellationToken cancellationToken)
    {
        FinancialStatement? fs = await repository.GetLastFinancialStatement(request.Event.Isin, request.Event.Date, cancellationToken);
        ResiliencePipeline pipeline = pipelineProvider.GetPipeline("DbUpdateConcurrencyException");

        if (fs is null)
        {
            return UpdateFsClosePriceErrorCodes.StatemtentNotFound;
        }

        fs.SetLastClosePrice(request.Event.ClosePrice, request.Event.Date);
        await pipeline.ExecuteAsync(
            async _ =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            },
            cancellationToken);
        return Response.Successful();
    }
}