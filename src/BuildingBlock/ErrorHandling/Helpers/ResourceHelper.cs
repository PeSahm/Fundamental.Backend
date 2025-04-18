using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace Fundamental.ErrorHandling.Helpers;

public static class ResourceHelper
{
    public const string DEFAULT_CULTURE = "en-US";

    /// <summary>
    ///     Get all error messages from all resource files in the given assembly and return them as a dictionary.
    /// </summary>
    /// <param name="assembly">Assembly to search resource files in.</param>
    /// <param name="cultureInfo">Desired language of messages (fallback: en-US).</param>
    /// <returns>A dictionary with error code keys and message values.</returns>
    public static Dictionary<string, string> GetAllErrorMessages(Assembly assembly, CultureInfo? cultureInfo = null)
    {
        cultureInfo ??= new CultureInfo(DEFAULT_CULTURE);

        string[] xmlResourceNames = assembly.GetManifestResourceNames()
            .Select(s => s.Replace(".resources", string.Empty))
            .ToArray();

        Dictionary<string, string> errorMessages = new();

        foreach (string xmlResourceName in xmlResourceNames)
        {
            ResourceManager resourceManager = new(xmlResourceName, assembly);
            ResourceSet? resourceSet = resourceManager.GetResourceSet(cultureInfo, true, true);

            if (resourceSet is null)
            {
                continue;
            }

            foreach (DictionaryEntry entry in resourceSet)
            {
                string? key = entry.Key.ToString();

                // Only include keys that have error code format (dd_ddd_ddd)
                if (key is not null && key.Length == 10 && key[2] == '_' && key[6] == '_')
                {
                    errorMessages.Add(key, entry.Value?.ToString() ?? string.Empty);
                }
            }
        }

        return errorMessages.OrderBy(kvp => kvp.Key).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
    }

    /// <summary>
    ///     Retrieves the default error message from a resource file based on the provided key and optional culture information.
    /// </summary>
    /// <typeparam name="TResource">The type of the resource file to access.</typeparam>
    /// <param name="key">The key of the default error message in the resource file.</param>
    /// <param name="cultureInfo">Optional. The culture for which the error message should be retrieved. If not provided, it defaults to null.</param>
    /// <returns>The default error message as a string. If the message is not found, returns an empty string.</returns>
    public static string GetDefaultErrorMessage<TResource>(string key, CultureInfo? cultureInfo = null)
    {
        cultureInfo ??= new CultureInfo(DEFAULT_CULTURE);
        ResourceManager resourceManager = new(typeof(TResource));
        return resourceManager.GetString(key, cultureInfo) ?? string.Empty;
    }
}