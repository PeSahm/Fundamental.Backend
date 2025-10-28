using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Helpers;

namespace IntegrationTests;

/// <summary>
/// Extension methods for Response types used in tests.
/// </summary>
internal static class ResponseExtensions
{
    /// <summary>
    /// Gets the error code as an integer from a Response.
    /// </summary>
    /// <param name="response">The response to get the error code from.</param>
    /// <returns>The error code as an integer.</returns>
    public static int ErrorCode(this Response response)
    {
        return response.Error.HasValue ? ErrorCodeHelper.ToInt(response.Error.Value.Code) : 0;
    }
}