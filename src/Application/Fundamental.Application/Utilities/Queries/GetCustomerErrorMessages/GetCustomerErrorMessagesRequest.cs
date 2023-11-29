using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Utilities.Queries.GetCustomerErrorMessages;

[HandlerCode(HandlerCode.GetCustomerErrorMessages)]
public record GetCustomerErrorMessagesRequest(string Client, string? Culture) : IRequest<Response<Dictionary<string, string>>>;