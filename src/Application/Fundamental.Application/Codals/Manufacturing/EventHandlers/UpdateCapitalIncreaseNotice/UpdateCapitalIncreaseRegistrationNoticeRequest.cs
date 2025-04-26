using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateCapitalIncreaseNotice;

[HandlerCode(HandlerCode.UpdateIncomeStatementCapital)]
public record UpdateCapitalIncreaseRegistrationNoticeRequest(CapitalIncreaseRegistrationNoticeCreated Event) : IRequest<Response>;