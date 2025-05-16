using Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.UnitOfWork
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        IQueryable<TEntity> GetAll(bool isIncludeDeleted = false);

        //IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors);

        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> InsertAsync(TEntity entity);

        Task DeleteAsync(Guid id, bool isPhysicalDelete = false);

        Task DeleteListAsync(List<Guid> ids, bool isPhysicalDelete = false);

        Task<TEntity> UpdateAsync(TEntity entity);
    }
}
