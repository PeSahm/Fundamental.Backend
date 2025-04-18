namespace Fundamental.Domain.Repositories.Base;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}