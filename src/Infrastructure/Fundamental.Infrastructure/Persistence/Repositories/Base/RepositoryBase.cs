using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using Fundamental.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Fundamental.Infrastructure.Persistence.Repositories.Base;

/// <inheritdoc />
public abstract class RepositoryBase : IRepository
{
    private readonly DbContext _dbContext;
    private readonly ISpecificationEvaluator _specificationEvaluator;

    protected RepositoryBase(DbContext dbContext)
    {
        _dbContext = dbContext;
        _specificationEvaluator = SpecificationEvaluator.Default;
    }

    public void Add<T>(T entity)
        where T : class
    {
        _dbContext.Set<T>().Add(entity);
    }

    public void AddRange<T>(IEnumerable<T> entities)
        where T : class
    {
        _dbContext.Set<T>().AddRange(entities);
    }

    public void Delete<T>(T entity)
        where T : class
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void DeleteRange<T>(IEnumerable<T> entities)
        where T : class
    {
        _dbContext.Set<T>().RemoveRange(entities);
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync<T, TId>(TId id, CancellationToken cancellationToken = default)
        where T : class
        where TId : notnull
    {
        return await _dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken: cancellationToken);
    }

    public async Task<T?> FirstOrDefaultAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> FirstOrDefaultAsync<T, TResult>(
        ISpecification<T, TResult> specification,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        return await ApplySpecification(specification).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<T?> SingleOrDefaultAsync<T>(
        ISingleResultSpecification<T> specification,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<TResult?> SingleOrDefaultAsync<T, TResult>(
        ISingleResultSpecification<T, TResult> specification,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        return await ApplySpecification(specification).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync<T>(CancellationToken cancellationToken = default)
        where T : class
    {
        return await _dbContext.Set<T>().ToListAsync(cancellationToken);
    }

    public async Task<List<T>> ListAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
    {
        List<T> queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<List<TResult>> ListAsync<T, TResult>(
        ISpecification<T, TResult> specification,
        CancellationToken cancellationToken = default
    )
        where T : class
    {
        List<TResult> queryResult = await ApplySpecification(specification).ToListAsync(cancellationToken);

        return specification.PostProcessingAction == null ? queryResult : specification.PostProcessingAction(queryResult).ToList();
    }

    public async Task<int> CountAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
    {
        return await ApplySpecification(specification, true).CountAsync(cancellationToken);
    }

    public async Task<int> CountAsync<T>(CancellationToken cancellationToken = default)
        where T : class
    {
        return await _dbContext.Set<T>().CountAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class
    {
        return await ApplySpecification(specification, true).AnyAsync(cancellationToken);
    }

    public async Task<bool> AnyAsync<T>(CancellationToken cancellationToken = default)
        where T : class
    {
        return await _dbContext.Set<T>().AnyAsync(cancellationToken);
    }

    /// <summary>
    /// Filters the entities  of <typeparamref name="T"/>, to those that match the encapsulated query logic of the
    /// <paramref name="specification"/>.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="evaluateCriteriaOnly">If yes, only criteria evaluators will be used.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>The filtered entities as an <see cref="IQueryable{T}"/>.</returns>
    protected virtual IQueryable<T> ApplySpecification<T>(ISpecification<T> specification, bool evaluateCriteriaOnly = false)
        where T : class
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification, evaluateCriteriaOnly);
    }

    /// <summary>
    /// Filters all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    /// <paramref name="specification"/>, from the database.
    /// <para>
    /// Projects each entity into a new form, being <typeparamref name="TResult" />.
    /// </para>
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <returns>The filtered projected entities as an <see cref="IQueryable{T}"/>.</returns>
    protected virtual IQueryable<TResult> ApplySpecification<T, TResult>(ISpecification<T, TResult> specification)
        where T : class
    {
        return _specificationEvaluator.GetQuery(_dbContext.Set<T>().AsQueryable(), specification);
    }
}