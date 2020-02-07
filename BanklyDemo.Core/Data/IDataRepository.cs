using BanklyDemo.Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BanklyDemo.Core.Data
{
    public interface IDataRepository<TEntity>
        where TEntity : BaseEntity
    {
        Task<TEntity> GetAsync(Guid entityId, bool allowNull = false);

        Task<IList<TEntity>> GetAllAsync();

        IList<TEntity> GetMany(Expression<Func<TEntity, bool>> where);

        Task<Guid> AddAsync(TEntity entity);

        Task AddManyAsync(IList<TEntity> entities);

        Task UpdateAsync(TEntity entity);

        Task UpdateManyAsync(IList<TEntity> entities);

        Task DeleteAsync(TEntity entity);

        Task DeleteManyAsync(IList<TEntity> entities);


    }
}
