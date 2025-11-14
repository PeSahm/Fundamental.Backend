using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.ErrorHandling;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Codals.Jobs.ExecuteStatementJob;

public class ExecuteStatementJobCommandHandler(
    ICodalService codalService,
    ILogger<ExecuteStatementJobCommandHandler> logger
) : IRequestHandler<ExecuteStatementJobRequest, Response>
{
    public async Task<Response> Handle(ExecuteStatementJobRequest request, CancellationToken cancellationToken)
    {
        GetStatementResponse? statement =
            await codalService.GetStatementByTraceNo(request.TraceNo, cancellationToken);

        if (statement is null)
        {
            return ExecuteStatementJobRequestErrorCodes.StatementNotFound;
        }

        try
        {
            await codalService.ProcessCodal(
                statement,
                request.ReportingType ?? statement.ReportingType,
                request.LetterPart,
                cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error processing codal for {@Model}", statement);
        }

        return Response.Successful();
    }
}