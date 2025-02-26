using Core.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Threading;

namespace Core.Persistence.Repositories;

public abstract class EfRepositoryBase<TEntity, TId, TContext> : IRepository<TEntity, TId>, IAsyncRepository<TEntity, TId>
    where TEntity : Entity<TId>
    where TContext : DbContext
{
    protected EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    protected   TContext Context { get; }



    public TEntity Add(TEntity entity)
    {
        entity.CreatedTime = DateTime.UtcNow;
        Context.Entry(entity).State = EntityState.Added;
        Context.SaveChanges();

        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        entity.CreatedTime = DateTime.UtcNow;
        Context.Entry(entity).State = EntityState.Added;

        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAync(IEnumerable<TEntity> entities,CancellationToken cancelationToken=default )
    {

        foreach (TEntity entity in entities)
        {
            entity.CreatedTime = DateTime.UtcNow;

        }
        await Context.Set<TEntity>().AddRangeAsync(entities,cancelationToken);
        await Context.SaveChangesAsync(cancelationToken);

        return entities;
   
    }

    public bool Any(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();

       
        if(enableTracking is false)
        {
            query = query.AsNoTracking();
        }


        if (filter is not null)
        {
            return query.Any(filter);
        }

        return query.Any();

    }

    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IQueryable<TEntity> query = Context.Set<TEntity>();


        if (enableTracking is false)
        {
            query = query.AsNoTracking();
        }


        if (filter is not null)
        {
            return await query.AnyAsync(filter,cancellationToken);
        }

        return await query.AnyAsync(cancellationToken);
    }

    public TEntity Delete(TEntity entity)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        Context.SaveChanges();

        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        Context.Entry(entity).State = EntityState.Deleted;
        await Context.SaveChangesAsync(cancellationToken);

        return entity;
    }

    public TEntity? Get(Expression<Func<TEntity, bool>> filter, bool enableTracking = true, bool include = true)
    {
        
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
