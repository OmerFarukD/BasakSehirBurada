using Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public abstract class EfRepositoryBase<TEntity, TId, TContext> : IRepository<TEntity, TId>, IAsyncRepository<TEntity, TId>

    where TEntity : Entity<TId>
    where TContext : DbContext
{
    public TEntity Add(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities)
    {
        throw new NotImplementedException();
    }

    public bool Any(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity Delete(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> filter, bool enableTracking = true, bool include = true)
    {
        throw new NotImplementedException();
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true, bool include = true)
    {
        throw new NotImplementedException();
    }

    public Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true, bool include = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> filter, bool enableTracking = true, bool include = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public TEntity Update(TEntity entity)
    {
        throw new NotImplementedException();
    }

    public Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
