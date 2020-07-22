using System;
using System.Collections.Generic;

namespace GHub.Data
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        void Create(TEntity item);
        TEntity FindById(int id);
        IAsyncEnumerable<TEntity> Get();
        IAsyncEnumerable<TEntity> Get(Func<TEntity, bool> predicate);
        void Remove(TEntity item);
        void Update(TEntity item);
    }
}