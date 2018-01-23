using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIENN.Services
{
    public interface ICrudService<TEntity,TKey>     where TEntity: class
                                                    where TKey: IEquatable<TKey>
    {
        //CRUD operations lonly
        Task<TEntity> Get(TKey id);
        Task<IQueryable<TEntity>> GetAll();
        Task Update(TEntity entity);
        Task Add(TEntity entity);
        Task Remove(TKey id);
    }
}
