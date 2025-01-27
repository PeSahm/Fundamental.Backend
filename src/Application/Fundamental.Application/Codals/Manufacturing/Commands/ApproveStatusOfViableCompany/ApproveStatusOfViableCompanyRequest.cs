using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.ApproveStatusOfViableCompany;

[HandlerCode(HandlerCode.ApproveStatusOfViableCompany)]
public sealed record ApproveStatusOfViableCompanyRequest(Guid Id, string SubsidiaryIsin) : IRequest<Response>;