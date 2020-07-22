using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GHub.Data
{
    public class EFGenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public EFGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public IAsyncEnumerable<TEntity> Get()
        {
            return _dbSet;
        }

        public async IAsyncEnumerable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            await foreach (var entity in _dbSet.AsAsyncEnumerable())
            {
                if (predicate(entity))
                {
                    yield return entity;
                }
            }

        }
        public TEntity FindById(int id)
        {
            return _dbSet.Find(id);
        }

        public void Create(TEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
        }
        public void Update(TEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}