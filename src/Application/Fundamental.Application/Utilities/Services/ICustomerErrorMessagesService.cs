using System.Globalization;
using Fundamental.ErrorHandling.Enums;

namespace Fundamental.Application.Utilities.Services;

public interface ICustomerErrorMessagesService
{
    /// <summary>
    ///     Get all customer error messages for the given client and culture.
    /// </summary>
    Dictionary<string, string> GetAll(Client client, CultureInfo culture);
}