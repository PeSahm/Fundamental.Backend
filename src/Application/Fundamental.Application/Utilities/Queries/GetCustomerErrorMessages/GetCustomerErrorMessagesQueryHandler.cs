using System.Globalization;
using Fundamental.Application.Utilities.Services;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Utilities.Queries.GetCustomerErrorMessages;

public class GetCustomerErrorMessagesQueryHandler : IRequestHandler<GetCustomerErrorMessagesRequest, Response<Dictionary<string, string>>>
{
    private readonly ICustomerErrorMessagesService _customerErrorMessagesService;

    public GetCustomerErrorMessagesQueryHandler(ICustomerErrorMessagesService customerErrorMessagesService)
    {
        _customerErrorMessagesService = customerErrorMessagesService;
    }

    public async Task<Response<Dictionary<string, string>>> Handle(
        GetCustomerErrorMessagesRequest request,
        CancellationToken cancellationToken
    )
    {
        Client? client = request.Client switch
        {
            "admin" => Client.AdminWeb,
            "web" => Client.CustomerWeb,
            _ => null,
        };

        if (client is null)
        {
            return GetCustomerErrorMessagesErrorCodes.InvalidClient;
        }

        CultureInfo cultureInfo = CultureInfo.GetCultureInfo(request.Culture ?? "en-US");

        return await Task.FromResult(_customerErrorMessagesService.GetAll(client.Value, cultureInfo));
    }
}