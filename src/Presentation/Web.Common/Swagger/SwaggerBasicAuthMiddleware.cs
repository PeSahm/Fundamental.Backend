using System.Net;
using System.Net.Http.Headers;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Fundamental.Web.Common.Swagger;

public class SwaggerBasicAuthMiddleware
{
    private readonly IConfiguration _configuration;
    private readonly RequestDelegate _next;

    public SwaggerBasicAuthMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? expectedUsername = _configuration.GetValue<string>("Swagger:Username");
        string? expectedPassword = _configuration.GetValue<string>("Swagger:Password");

        if (string.IsNullOrWhiteSpace(expectedUsername) && string.IsNullOrWhiteSpace(expectedPassword))
        {
            await _next.Invoke(context).ConfigureAwait(false);
            return;
        }

        string? authHeader = context.Request.Headers["Authorization"];

        if (authHeader != null && authHeader.StartsWith("Basic "))
        {
            // Get the credentials from request header
            AuthenticationHeaderValue header = AuthenticationHeaderValue.Parse(authHeader);
            byte[] inBytes = Convert.FromBase64String(header.Parameter!);
            string[] credentials = Encoding.UTF8.GetString(inBytes).Split(':');
            string username = credentials[0];
            string password = credentials[1];

            // validate credentials
            if (username.Equals(expectedUsername) && password.Equals(expectedPassword))
            {
                await _next.Invoke(context).ConfigureAwait(false);
                return;
            }
        }

        context.Response.Headers["WWW-Authenticate"] = "Basic";
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
    }
}