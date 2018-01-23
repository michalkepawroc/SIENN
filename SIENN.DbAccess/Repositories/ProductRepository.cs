using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Text;

namespace SIENN.DbAccess.Repositories
{
    public class ProductRepository<TEntity, TKey> : GenericRepository<TEntity, TKey> 
                                                                                        where TEntity : class
                                                                                        where TKey : IEquatable<TKey>
    {
        public ProductRepository(DbContext context) : base(context)
        {
        }
    }
}
