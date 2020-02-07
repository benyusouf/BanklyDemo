using BanklyDemo.Core.Common.Models;
using BanklyDemo.Core.Data;
using BanklyDemo.Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BanklyDemo.Data.Repositories
{
    internal abstract class DataRepository<TEntity> : IDataRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly BanklyDemoDbContext _dbContext;
        protected DataRepository(BanklyDemoDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public virtual async Task<Guid> AddAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            await _dbContext.AddAsync(entity);

            await _dbContext.SaveChangesAsync();

            return entity.Id;
        }

        public virtual async Task DeleteAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));

            _dbContext.Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteManyAsync(IList<TEntity> entities)
        {
            ArgumentGuard.NotNull(entities, nameof(entities));

            _dbContext.RemoveRange(entities);

            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<TEntity> GetAsync(Guid entityId, bool allowNull = false)
        {
            ArgumentGuard.NotNull(entityId, nameof(entityId));

            return await _dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == entityId);
        }

        public virtual IList<TEntity> GetMany(Expression<Func<TEntity, bool>> where)
        {
            return _dbContext.Set<TEntity>().AsNoTracking().Where(where).ToList();
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {
            ArgumentGuard.NotNull(entity, nameof(entity));
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateManyAsync(IList<TEntity> entities)
        {
            ArgumentGuard.NotNullOrEmpty(entities, nameof(entities));

            entities.ForEach(e => {
                _dbContext.Set<TEntity>().Attach(e);
                _dbContext.Entry(e).State = EntityState.Modified;
            });
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddManyAsync(IList<TEntity> entities)
        {
            ArgumentGuard.NotNullOrEmpty(entities, nameof(entities));

            await _dbContext.AddRangeAsync(entities);

            await _dbContext.SaveChangesAsync();
        }
    }
}
