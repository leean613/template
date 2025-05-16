using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Audits
{
    public interface IAuditHelper
    {
        void ApplyConcepts(EntityEntry entry);
    }
}