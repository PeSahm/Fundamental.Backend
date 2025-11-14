using System.Data;
using System.Linq.Expressions;
using Ardalis.Specification;
using RepoDb;

namespace Fundamental.Domain.Repositories.Base;

/// <summary>
///     <para>
///         An <see cref="IReadRepository" /> can be used to query instances of entities.
///     </para>
///     <para>
///         An <see cref="ISpecification{T}" /> (or derived) is used to encapsulate the LINQ queries against the database.
///     </para>
/// </summary>
public interface IReadRepository
{
    /// <summary>
    ///     Finds an entity with the given primary key value.
    /// </summary>
    /// <param name="id">The value of the primary key for the entity to be found.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <typeparam name="TId">The type of primary key.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />, or <see langword="null" />.
    /// </returns>
    Task<T?> GetByIdAsync<T, TId>(TId id, CancellationToken cancellationToken = default)
        where T : class
        where TId : notnull;

    /// <summary>
    ///     Returns the first element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />, or <see langword="null" />.
    /// </returns>
    Task<T?> FirstOrDefaultAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns the first element of a sequence, or a default value if the sequence contains no elements.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="TResult" />, or <see langword="null" />.
    /// </returns>
    Task<TResult?> FirstOrDefaultAsync<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more
    ///     than one element in the sequence.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="T" />, or <see langword="null" />.
    /// </returns>
    Task<T?> SingleOrDefaultAsync<T>(ISingleResultSpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns the only element of a sequence, or a default value if the sequence is empty; this method throws an exception if there is more
    ///     than one element in the sequence.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains the <typeparamref name="TResult" />, or <see langword="null" />.
    /// </returns>
    Task<TResult?> SingleOrDefaultAsync<T, TResult>(
        ISingleResultSpecification<T, TResult> specification,
        CancellationToken cancellationToken = default
    )
        where T : class;

    /// <summary>
    ///     Finds all entities of <typeparamref name="T" /> from the database.
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync<T>(CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Finds all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    ///     <paramref name="specification" />, from the database.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a <see cref="List{T}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<T>> ListAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Finds all entities of <typeparamref name="T" />, that matches the encapsulated query logic of the
    ///     <paramref name="specification" />, from the database.
    ///     <para>
    ///         Projects each entity into a new form, being <typeparamref name="TResult" />.
    ///     </para>
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the projection.</typeparam>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <returns>
    ///     A task that represents the asynchronous operation.
    ///     The task result contains a <see cref="List{TResult}" /> that contains elements from the input sequence.
    /// </returns>
    Task<List<TResult>> ListAsync<T, TResult>(ISpecification<T, TResult> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns a number that represents how many entities satisfy the encapsulated query logic
    ///     of the <paramref name="specification" />.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the
    ///     number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns the total number of records.
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains the
    ///     number of elements in the input sequence.
    /// </returns>
    Task<int> CountAsync<T>(CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns a boolean that represents whether any entity satisfy the encapsulated query logic
    ///     of the <paramref name="specification" /> or not.
    /// </summary>
    /// <param name="specification">The encapsulated query logic.</param>
    /// <param name="cancellationToken">A <see cref="CancellationToken" /> to observe while waiting for the task to complete.</param>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the
    ///     source sequence contains any elements; otherwise, false.
    /// </returns>
    Task<bool> AnyAsync<T>(ISpecification<T> specification, CancellationToken cancellationToken = default)
        where T : class;

    /// <summary>
    ///     Returns a boolean whether any entity exists or not.
    /// </summary>
    /// <typeparam name="T">The type of entity being operated on by this method.</typeparam>
    /// <returns>
    ///     A task that represents the asynchronous operation. The task result contains true if the
    ///     source sequence contains any elements; otherwise, false.
    /// </returns>
    Task<bool> AnyAsync<T>(CancellationToken cancellationToken = default)
        where T : class;

    Task<int> BulkMergeAsync<T, TResult>(
        string tableName,
        IEnumerable<T> entities,
        Expression<Func<T, TResult>>? qualifiers = null,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default
    )
        where T : class;

    Task<int> BulkMergeAsync<T>(
        string tableName,
        IEnumerable<T> entities,
        IEnumerable<Field> fields,
        IDbTransaction? transaction = null,
        CancellationToken cancellationToken = default
    )
        where T : class;
}