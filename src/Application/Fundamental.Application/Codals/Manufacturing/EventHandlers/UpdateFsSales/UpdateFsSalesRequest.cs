using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsSales;

[HandlerCode(HandlerCode.UpdateFinancialStatementSales)]
public sealed record UpdateFsSalesRequest(MonthlyActivityUpdated Event) : IRequest<Response>;