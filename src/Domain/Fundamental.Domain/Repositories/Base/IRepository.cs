using Ardalis.Specification;

namespace Fundamental.Domain.Repositories.Base;

/// <summary>
///     <para>
///         An <see cref="IRepository" /> can be used to query and save instances of entities.
///     </para>
///     <para>
///         An <see cref="ISpecification{T}" /> (or derived) is used to encapsulate the LINQ queries against the database.
///     </para>
/// </summary>
public interface IRepository : IReadRepository
{
    /// <summary>
    ///     Adds an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    void Add<T>(T entity)
        where T : class;

    /// <summary>
    ///     Adds the given entities in the database.
    /// </summary>
    /// <param name="entities">Entities to add.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    void AddRange<T>(IEnumerable<T> entities)
        where T : class;

    /// <summary>
    ///     Removes an entity in the database.
    /// </summary>
    /// <param name="entity">The entity to delete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    void Delete<T>(T entity)
        where T : class;

    /// <summary>
    ///     Removes the given entities in the database.
    /// </summary>
    /// <param name="entities">The entities to remove.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    void DeleteRange<T>(IEnumerable<T> entities)
        where T : class;

    /// <summary>
    ///     Persists changes to the database.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}