using Ardalis.Specification;

namespace Fundamental.Domain.Repositories.Base;

public interface IRepository<T> : IReadRepositoryBase<T>
    where T : class
{
    T Add(T entity);

    void AddRange(IEnumerable<T> entities);

    void Delete(T entity);

    void DeleteRange(IEnumerable<T> entities);
}