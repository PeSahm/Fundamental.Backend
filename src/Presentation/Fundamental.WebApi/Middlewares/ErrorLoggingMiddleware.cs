using Fundamental.Application.Services;

namespace Fundamental.WebApi.Middlewares;

public class ErrorLoggingMiddleware(RequestDelegate next, ILogger<ErrorLoggingMiddleware> logger, IIpService ipService)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await next(httpContext);

        switch (httpContext.Response.StatusCode)
        {
            case 401:
            case 403:
                string? ipAddress = ipService.GetRawRemoteIpAddress();
                string? authorizationHeader = httpContext.Request.Headers["Authorization"];

                logger.LogWarning(
                    "HTTP status code '{StatusCode}' returned for authorization header '{AuthorizationHeader}' sent from '{RemoteIpAddress}'",
                    httpContext.Response.StatusCode,
                    authorizationHeader,
                    ipAddress);
                break;
        }
    }
}