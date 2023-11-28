using System.Data.Common;
using FluentValidation.Results;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Common.Validators;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Helpers;
using Fundamental.ErrorHandling.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Fundamental.Application.Common.PipelineBehaviors;

public class CommonErrorsPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : struct, IResponse
{
    private readonly ILogger<CommonErrorsPipelineBehavior<TRequest, TResponse>> _logger;
    private readonly IRequestValidator<TRequest> _requestValidator;

    public CommonErrorsPipelineBehavior(
        ILogger<CommonErrorsPipelineBehavior<TRequest, TResponse>> logger,
        IRequestValidator<TRequest> requestValidator
    )
    {
        _logger = logger;
        _requestValidator = requestValidator;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        HandlerCode handlerCode = request.GetHandlerCode();
        string handlerNumber = ErrorCodeHelper.Format((int)handlerCode);

        List<ValidationFailure> errorList = await _requestValidator.ValidateAsync(request, cancellationToken);

        TResponse response;

        if (errorList.Any())
        {
            response = new TResponse
            {
                Success = false,
                Error = CommonErrorCode.ValidationFailed.ForRequest(
                    request,
                    new Dictionary<string, string>
                    {
                        { "message", errorList[0].ErrorMessage },
                    }
                ),
            };
            _logger.LogWarning(
                "HANDLER.ERROR.VALIDATION --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}\n    {@Errors}",
                handlerNumber,
                handlerCode,
                ErrorCodeHelper.Format(response.Error.Value.Code),
                errorList);

            return response;
        }

        try
        {
            response = await next();
        }
        catch (Exception e) when (e is ICodedException codedException)
        {
            response = new TResponse
            {
                Success = false, Error = codedException.GetCommonErrorCode().ForRequest(request),
            };
            LogError("HANDLER.ERROR.EXPECTED", e, handlerCode, handlerNumber, response.Error.Value.Code);
        }
        catch (Exception e) when (e.InnerException is DbException)
        {
            response = new TResponse
            {
                Success = false, Error = CommonErrorCode.DatabaseError.ForRequest(request),
            };
            LogError("HANDLER.ERROR.DATABASE", e, handlerCode, handlerNumber, response.Error.Value.Code);
        }
        catch (Exception e)
        {
            response = new TResponse
            {
                Success = false, Error = CommonErrorCode.UnexpectedError.ForRequest(request),
            };
            LogError("HANDLER.ERROR.UNEXPECTED", e, handlerCode, handlerNumber, response.Error.Value.Code);
        }

        return response;
    }

    private void LogError(string name, Exception e, HandlerCode handlerCode, string handlerNumber, int errorCode)
    {
        _logger.LogError(
            e,
            "{Name} --- {HandlerCode} ({HandlerName}) --- ErrorCode: {ErrorCode}",
            name,
            handlerNumber,
            handlerCode,
            ErrorCodeHelper.Format(errorCode));
    }
}