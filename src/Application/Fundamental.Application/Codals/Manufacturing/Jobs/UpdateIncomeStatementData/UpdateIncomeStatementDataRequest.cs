using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateIncomeStatementData;

[HandlerCode(HandlerCode.UpdateIncomeStatementData)]
public sealed record UpdateIncomeStatementDataRequest(uint DaysBefore) : IRequest<Response>;