using System.Linq.Expressions;

namespace JobCounselor.Infrastructure.Repositories;

/// <summary>
/// Generic repository abstraction for CRUD operations on entities.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public interface IRepository<T> where T : class
{
    /// <summary>
    /// Adds the given entity to the data store.
    /// </summary>
    Task AddAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Updates the given entity in the data store.
    /// </summary>
    Task UpdateAsync(T entity, CancellationToken cancellationToken);

    /// <summary>
    /// Retrieves an entity by its identifier using the provided key selector.
    /// </summary>
    Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken);

    /// <summary>
    /// Returns an <see cref="IQueryable{T}"/> for additional query composition.
    /// </summary>
    IQueryable<T> Query();
}
