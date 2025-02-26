using Core.Persistence.Entities;
using System.Linq.Expressions;

namespace Core.Persistence.Repositories;

public interface IRepository<TEntity, TId> where TEntity: Entity<TId>
{
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Delete(TEntity entity);


    List<TEntity> GetAll(Expression<Func<TEntity,bool>>? filter=null,bool enableTracking = true, bool include =true);

    TEntity? Get(Expression<Func<TEntity, bool>> filter, bool enableTracking = true, bool include = true);

    bool Any(Expression<Func<TEntity, bool>>? filter = null, bool enableTracking = true);


}
