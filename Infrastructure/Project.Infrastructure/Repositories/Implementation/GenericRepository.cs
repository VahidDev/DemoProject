using Microsoft.EntityFrameworkCore;
using Project.Core.Utilities.Results;
using Project.Infrastructure.DAL;
using Project.Infrastructure.Repositories.Abstraction;
using System.Linq.Expressions;

namespace Project.Infrastructure.Repositories.Implementation
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> 
        where TEntity : class
    {
        private readonly AppDbContext _dbContext;

        protected DbSet<TEntity> DbSet { get; set; }

        public GenericRepository(AppDbContext dbContext)
        {
            DbSet = dbContext.Set<TEntity>();
            _dbContext = dbContext;
        }

        public Result Add(TEntity item)
        {
            var result = new Result();

            try
            {
                DbSet.Add(item);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message; 
            }

            return result;
        }

        public bool Any(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Any(predicate);
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.FirstOrDefault(filter);
        }

        public IQueryable<TEntity> GetAllAsNoTracking(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null ? DbSet.AsNoTracking() : DbSet.Where(filter).AsNoTracking();
        }

        public Result Update(TEntity item)
        {
            var result = new Result();

            try
            {
                DbSet.Update(item);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }

        public Result AddRange(ICollection<TEntity> items)
        {
            var result = new Result();

            try
            {
                DbSet.AddRange(items);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Error = ex.Message;
            }

            return result;
        }
    }
}
