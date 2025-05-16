using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkCore.UnitOfWork
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        //Review: Property moved to IUnitOfWork
        //TContext Context { get; }

        Task<int> CompleteAsync(bool ensureAutoHistory = false, params IUnitOfWork[] unitOfWorks);
    }
}