using DotNetCore.CAP;
using Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsClosePrice;
using Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateFsSales;
using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.Domain.Common.Constants;
using Fundamental.Domain.Prices.Events;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers;

public class EventDataDispatcher(IMediator mediator) : ICapSubscribe
{
    [CapSubscribe(EventsAddress.ClosePrice.PRICE_UPDATE, Group = EventsAddress.FinancialStatement.FINANCIAL_STATEMENT_EVENT_GROUP)]
    public async Task UpdateFsClosePrice(ClosePriceUpdated eventData)
    {
        await mediator.Send(new UpdateFsClosePriceRequest(eventData));
    }

    [CapSubscribe(EventsAddress.MonthlyActivity.MONTHLY_ACTIVITY_UPDATE,
        Group = EventsAddress.FinancialStatement.FINANCIAL_STATEMENT_EVENT_GROUP)]
    public async Task UpdateFsSales(MonthlyActivityUpdated eventData)
    {
        await mediator.Send(new UpdateFsSalesRequest(eventData));
    }
}