using Entities.Interfaces;

namespace EntityFrameworkCore.UnitOfWork
{
    public interface IRepositoryFactory
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IEntity;
    }
}