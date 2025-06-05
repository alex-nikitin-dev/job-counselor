using Microsoft.EntityFrameworkCore;
using JobCounselor.Infrastructure.Data;

namespace JobCounselor.Infrastructure.Repositories;

/// <summary>
/// Generic Entity Framework based repository implementation.
/// </summary>
/// <typeparam name="T">Entity type.</typeparam>
public class EfRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    /// <summary>
    /// Initializes the repository with the provided <see cref="AppDbContext"/>.
    /// </summary>
    public EfRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }

    public IQueryable<T> Query() => _dbSet.AsQueryable();

    public async Task AddAsync(T entity, CancellationToken cancellationToken)
    {
        await _dbSet.AddAsync(entity, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
    {
        _dbSet.Update(entity);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken)
    {
        return await _dbSet.FindAsync(new[] { id }, cancellationToken);
    }
}
