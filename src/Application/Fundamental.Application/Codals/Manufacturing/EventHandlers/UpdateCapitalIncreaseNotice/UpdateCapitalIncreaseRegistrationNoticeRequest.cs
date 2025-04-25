using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.EventHandlers.UpdateCapitalIncreaseNotice;

public record UpdateCapitalIncreaseRegistrationNoticeRequest(CapitalIncreaseRegistrationNoticeCreated Event) : IRequest<Response>;