using Fundamental.Domain.Prices.Events;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsClosePrice;

[HandlerCode(HandlerCode.UpdateFinancialStatementClosePrice)]
public sealed record UpdateFsClosePriceRequest(ClosePriceUpdated Event) : IRequest<Response>;