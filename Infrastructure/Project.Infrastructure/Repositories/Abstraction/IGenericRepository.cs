using Project.Core.Utilities.Results;
using System.Linq.Expressions;

namespace Project.Infrastructure.Repositories.Abstraction
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        IQueryable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> filter = null);
        bool Any(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Expression<Func<TEntity, bool>> filter);
        Result Add(TEntity item);
        Result AddRange(ICollection<TEntity> items);
        Result Update(TEntity item);
    }
}
