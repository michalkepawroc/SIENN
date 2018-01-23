using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIENN.Services
{
    public class CrudService<TEntity, TKey> : ICrudService<TEntity, TKey> where TEntity : class
                                                                            where TKey : IEquatable<TKey>
    {
        private readonly IGenericRepository<TEntity, TKey> _repository;

        public CrudService(DbContext dbContext)
        {
            this._repository = new GenericRepository<TEntity, TKey>(dbContext);
        }

        public async Task Add(TEntity entity)
        {
            await _repository.Add(entity);
        }

        public async Task<TEntity> Get(TKey id)
        {
            return await _repository.Get(id);
        }

        public async Task<IQueryable<TEntity>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task Remove(TKey id)
        {
            await _repository.Remove(id);
        }

        public async Task Update(TEntity entity)
        {
             await _repository.Update(entity);
        }
        
    }
}
