using Fundamental.Application.Codals.Services;
using Fundamental.Application.Codals.Services.Models.CodelServiceModels;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ForceUpdateStatements;

public sealed class ForceUpdateStatementRequestCommandHandler(ICodalService codalService)
    : IRequestHandler<ForceUpdateStatementRequest, Response>
{
    public async Task<Response> Handle(ForceUpdateStatementRequest request, CancellationToken cancellationToken)
    {
        GetStatementResponse? theStatement = await codalService.GetStatementByTraceNo(request.TraceNo, cancellationToken);

        if (theStatement is null)
        {
            return ForceUpdateStatementErrorCodes.InvalidStatement;
        }

        await codalService.ProcessCodal(theStatement, request.LetterPart, cancellationToken);

        return Response.Successful();
    }
}