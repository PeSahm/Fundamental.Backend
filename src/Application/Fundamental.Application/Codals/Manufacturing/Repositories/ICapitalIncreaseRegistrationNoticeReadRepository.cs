using Fundamental.Domain.Codals.Manufacturing.Entities;

namespace Fundamental.Application.Codals.Manufacturing.Repositories;

public interface ICapitalIncreaseRegistrationNoticeReadRepository
{
    Task<CapitalIncreaseRegistrationNotice?> GetByTraceNo(ulong traceNo, CancellationToken cancellationToken);
}