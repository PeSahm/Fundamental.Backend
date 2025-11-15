using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.RejectStatusOfViableCompany;

[HandlerCode(HandlerCode.RejectStatusOfViableCompany)]
public sealed record RejectStatusOfViableCompanyRequest(Guid Id) : IRequest<Response>;