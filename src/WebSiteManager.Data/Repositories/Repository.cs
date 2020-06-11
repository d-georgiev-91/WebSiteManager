using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace WebSiteManager.Data.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = dbContext.Set<TEntity>();
        }

        public IEnumerable<TEntity> Get() => _dbSet;

        public IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate) => _dbSet.Where(predicate);

        public void Add(TEntity entity) => ChangeState(entity, EntityState.Added);

        public void Delete(TEntity entity) => ChangeState(entity, EntityState.Deleted);

        public void Update(TEntity entity) => ChangeState(entity, EntityState.Modified);

        private void ChangeState(TEntity entity, EntityState state)
        {
            var entry = _dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                _dbSet.Attach(entity);
            }

            entry.State = state;
        }

        public void Dispose() => _dbContext?.Dispose();
    }
}