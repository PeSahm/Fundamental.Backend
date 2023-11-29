using System.Globalization;
using FluentValidation;

namespace Fundamental.Application.Utilities.Queries.GetCustomerErrorMessages;

public class GetCustomerErrorMessagesValidator : AbstractValidator<GetCustomerErrorMessagesRequest>
{
    public GetCustomerErrorMessagesValidator()
    {
        RuleFor(r => r.Client)
            .NotEmpty()
            .WithMessage("Client is required.");

        RuleFor(r => r.Culture)
            .Must(c => c == null || CultureExists(c))
            .WithMessage("Culture is invalid. Example: 'en-US'.");
    }

    private static bool CultureExists(string c)
    {
        return CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Any(c2 => c2.Name.Equals(c, StringComparison.InvariantCultureIgnoreCase));
    }
}