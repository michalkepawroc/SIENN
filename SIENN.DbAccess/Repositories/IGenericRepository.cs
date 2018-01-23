using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SIENN.DbAccess.Repositories
{
    public interface IGenericRepository<TEntity, TKey>    where TEntity : class
                                                    where TKey : IEquatable<TKey>
    {
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        
        //Task<DbSet<TEntity>> GetAll2();
        IEnumerable<TEntity> GetRange(int start, int count);
        IEnumerable<TEntity> GetRange(int start, int count, Expression<Func<TEntity, bool>> predicate);

        //Task<DbSet<TEntity>> GetRangePaged()
        int Count();

        Task<DbSet<TEntity>> GetAll();
        Task<TEntity> Get(TKey id);
        Task Update(TEntity entity);
        Task Add(TEntity entity);
        Task Remove(TKey id);
    }
}
