using System.Globalization;
using Fundamental.Application.Utilities.Services;
using Fundamental.ErrorHandling.Enums;
using Fundamental.ErrorHandling.Helpers;
using Fundamental.Infrastructure.Resources;

namespace Fundamental.Infrastructure.Services;

/// <inheritdoc />
public class CustomerErrorMessagesService : ICustomerErrorMessagesService
{
    private const string DEFAULT_ERROR_MESSAGE_KEY = "default_error_message";

    public Dictionary<string, string> GetAll(Client client, CultureInfo culture)
    {
        Dictionary<string, string> errorMessages = ResourceHelper.GetAllErrorMessages(typeof(CommonErrors).Assembly, culture);
        Dictionary<string, string> result = new()
        {
            { DEFAULT_ERROR_MESSAGE_KEY, ResourceHelper.GetDefaultErrorMessage<CommonErrors>(DEFAULT_ERROR_MESSAGE_KEY, culture) }
        };

        foreach (KeyValuePair<string, string> errorMessage in errorMessages)
        {
            if (errorMessage.Key.StartsWith("00_000_"))
            {
                result[errorMessage.Key] = errorMessage.Value;
            }
            else
            {
                Client clientOfError = ErrorCodeHelper.GetClientOfErrorCode(errorMessage.Key);

                if (clientOfError == client || clientOfError == Client.CustomerShared)
                {
                    result[errorMessage.Key] = errorMessage.Value;
                }
            }
        }

        return result;
    }
}