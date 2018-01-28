﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using OnlineStore.Data.Contracts;
using OnlineStore.Data.Entities;

namespace OnlineStore.Data
{
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : EntityBase<TKey>
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public Task<TEntity> GetAsync(TKey id)
        {
            return _dbSet.FindAsync(id);
        }

        public IQueryable<TEntity> GetAll(int skip, int take)
        {
            return _dbSet.OrderBy(q => q.Id).Skip(skip).Take(take);
        }

        public IQueryable<TEntity> GetAll(int skip, int take, Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll(skip, take).Where(predicate);
        }

        public void Add(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async void Update(TKey id)
        {
            var entity = await GetAsync(id);

            Delete(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Entry(entity).State = EntityState.Deleted;
        }

        public Task SaveChangeAsync()
        {
            return _dbContext.SaveChangesAsync();
        }
    }
}
