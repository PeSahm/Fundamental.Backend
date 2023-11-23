using Fundamental.Application.Services;

namespace Fundamental.WebApi.Middlewares;

public class ErrorLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger _logger;
    private readonly IIpService _ipService;

    public ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger, IIpService ipService)
    {
        _next = next;
        _ipService = ipService;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        await _next(httpContext);

        switch (httpContext.Response.StatusCode)
        {
            case 401:
            case 403:
                string? ipAddress = _ipService.GetRawRemoteIpAddress();
                string? authorizationHeader = httpContext.Request.Headers["Authorization"];

                _logger.LogWarning(
                    "HTTP status code '{StatusCode}' returned for authorization header '{AuthorizationHeader}' sent from '{RemoteIpAddress}'",
                    httpContext.Response.StatusCode,
                    authorizationHeader,
                    ipAddress);
                break;
        }
    }
}