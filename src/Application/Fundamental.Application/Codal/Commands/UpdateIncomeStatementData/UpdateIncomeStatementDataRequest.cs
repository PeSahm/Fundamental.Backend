using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codal.Commands.UpdateIncomeStatementData;

[HandlerCode(HandlerCode.UpdateIncomeStatementData)]
public sealed record UpdateIncomeStatementDataRequest(int Days) : IRequest<Response>;