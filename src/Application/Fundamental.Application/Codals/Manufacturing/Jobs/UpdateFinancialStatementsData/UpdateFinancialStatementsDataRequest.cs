using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;

[HandlerCode(HandlerCode.UpdateFinancialStatementsData)]
public sealed record UpdateFinancialStatementsDataRequest : IRequest<Response>;