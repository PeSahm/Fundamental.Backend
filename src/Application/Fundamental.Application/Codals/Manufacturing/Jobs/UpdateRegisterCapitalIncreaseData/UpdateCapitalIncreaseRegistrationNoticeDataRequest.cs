using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateRegisterCapitalIncreaseData;

public record UpdateCapitalIncreaseRegistrationNoticeDataRequest(uint DaysBeforeToday) : IRequest;