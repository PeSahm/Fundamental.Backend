using Ardalis.Specification.EntityFrameworkCore;
using Fundamental.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Persistence.Repositories.Base;

public abstract class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
    where TEntity : class
{
    private readonly DbContext _context;

    protected Repository(DbContext context)
        : base(context)
    {
        _context = context;
    }

    public TEntity Add(TEntity entity)
    {
        return _context.Add(entity).Entity;
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _context.AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _context.RemoveRange(entities);
    }
}