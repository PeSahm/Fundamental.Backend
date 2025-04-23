using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateRegisterCapitalIncreaseData;

public record UpdateCapitalIncreaseRegistrationNoticeDataRequest(int DaysBeforeToday) : IRequest;