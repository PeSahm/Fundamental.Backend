using System.Text.Json;
using Fundamental.Application.Common.Extensions;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Helpers;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Common.PipelineBehaviors;

public class LogRequestsPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : struct, IResponse
{
    private readonly ILogger<LogRequestsPipelineBehavior<TRequest, TResponse>> _logger;

    public LogRequestsPipelineBehavior(
        ILogger<LogRequestsPipelineBehavior<TRequest, TResponse>> logger
    )
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        HandlerCode handlerCode = request.GetHandlerCode();
        string handlerNumber = ErrorCodeHelper.Format((int)handlerCode);

        LogRequestStarting(handlerCode, handlerNumber);

        _logger.LogDebug("HANDLER.REQUEST.DETAILS --- {Name} --- {Request}", request.GetType().Name, JsonSerializer.Serialize(request));

        TResponse response = await next();

        if (response.Success)
        {
            _logger.LogInformation(
                "HANDLER.RESPONSE.SUCCESS --- {HandlerCode} ({HandlerName})",
                handlerNumber,
                handlerCode);
        }
        else
        {
            _logger.LogWarning(
                "HANDLER.RESPONSE.FAILURE --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}",
                handlerNumber,
                handlerCode,
                response.Error!.Value.Code);
        }

        return response;
    }

    private void LogRequestStarting(HandlerCode handlerCode, string handlerNumber)
    {
        _logger.LogInformation(
            "HANDLER.REQUEST.STARTING --- {HandlerCode} ({HandlerName})",
            handlerNumber,
            handlerCode);
    }
}