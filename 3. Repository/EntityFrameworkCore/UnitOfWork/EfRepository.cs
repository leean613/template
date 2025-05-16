using Common.Exceptions;
using Common.Extentions;
using Entities;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFrameworkCore.UnitOfWork
{
    public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        public DbContext Context;

        public virtual DbSet<TEntity> Table => Context.Set<TEntity>();

        public EfRepository(DbContext context)
        {
            Context = context;
        }

        public IQueryable<TEntity> GetAll(bool isIncludeDeleted = false)
        {
            return !isIncludeDeleted ? Table : Context.Set<TEntity>().IgnoreQueryFilters();
        }

        //public IQueryable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] propertySelectors)
        //{
        //    var query = Table.AsQueryable();

        //    if (!propertySelectors.IsNullOrEmpty())
        //    {
        //        foreach (var propertySelector in propertySelectors)
        //        {
        //            query = query.Include(propertySelector);
        //        }
        //    }

        //    return query;
        //}

        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public Task<TEntity> InsertAsync(TEntity entity)
        {
            return Task.FromResult(Insert(entity));
        }

        public TEntity Insert(TEntity entity)
        {
            SetId(entity);
            CheckAndSetDefaultValues(entity);

            Context.Entry(entity).State = EntityState.Added;

            return Table.Add(entity).Entity;
        }

        private void SetId(TEntity entity)
        {
            entity.Id = Guid.NewGuid();
        }

        private static void CheckAndSetDefaultValues(TEntity entity)
        {
            //var isDeletedProperty = entity.GetType().GetProperty("IsDeleted");
            //if (isDeletedProperty != null)
            //{
            //    isDeletedProperty.SetValue(entity, false);
            //}

            //var createDateProperty = entity.GetType().GetProperty("CreatedDate");
            //if (createDateProperty != null)
            //{
            //    createDateProperty.SetValue(entity, DateTime.Now);
            //}

            //var createUserProperty = entity.GetType().GetProperty("CreatedUser");
            //if (createUserProperty != null)
            //{
            //    createUserProperty.SetValue(entity, SingletonDependency<IUserSession>.Instance.GetUserId());
            //}
        }

        public virtual Task DeleteAsync(Guid id, bool isPhysicalDelete = false)
        {
            Delete(id, isPhysicalDelete);
            return Task.FromResult(0);
        }


        public Task DeleteListAsync(List<Guid> ids, bool isPhysicalDelete = false)
        {
            foreach (var item in ids)
            {
                Delete(item, isPhysicalDelete);
            }
            return Task.FromResult(0);
        }

        public void Delete(Guid id, bool isPhysicalDelete = false)
        {
            var entity = GetFromChangeTrackerOrNull(id);
            if (entity != null)
            {
                Delete(entity, isPhysicalDelete);
                return;
            }

            entity = Get(id, isPhysicalDelete);

            if (entity != null)
            {
                Delete(entity, isPhysicalDelete);
            }

            //Could not found the entity, do nothing.
        }

        private TEntity GetFromChangeTrackerOrNull(Guid id)
        {
            var entry = Context.ChangeTracker.Entries()
                .FirstOrDefault(
                    ent =>
                        ent.Entity is TEntity &&
                        EqualityComparer<Guid>.Default.Equals(id, ((TEntity)ent.Entity).Id)
                );

            return entry?.Entity as TEntity;
        }

        public void Delete(TEntity entity, bool isPhysicalDelete = false)
        {
            AttachIfNot(entity);
            if(!isPhysicalDelete)
            {
                CancelDeletionForSoftDelete(entity);
            }    
            else
            {
                Table.Remove(entity);
            }
        }

        protected virtual void AttachIfNot(TEntity entity)
        {
            var entry = Context.ChangeTracker.Entries().FirstOrDefault(ent => ent.Entity == entity);
            if (entry != null)
            {
                return;
            }

            Table.Attach(entity);
        }

        public TEntity Get(Guid id, bool isPhysicalDelete = false)
        {
            var entity = GetAll(isPhysicalDelete).FirstOrDefault(x => x.Id == id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public Task<TEntity> UpdateAsync(TEntity entity)
        {
            return Task.FromResult(Update(entity));
        }

        public TEntity Update(TEntity entity)
        {
            AttachIfNot(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        private void CancelDeletionForSoftDelete(TEntity entity)
        {
            if (!(entity is IStatusableEntity))
            {
                return;
            }

            Context.Entry(entity).State = EntityState.Modified;
            entity.As<IStatusableEntity>().IsAlive = EntityStatus.Delete;
        }
    }
}
