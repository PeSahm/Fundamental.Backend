using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateTheStatusOfViableCompanyData;

[HandlerCode(HandlerCode.UpdateTheStatusOfViableCompany)]
public sealed record UpdateTheStatusOfViableCompanyDataReqeust : IRequest<Response>;