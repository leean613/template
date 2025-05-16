using System;
using Common.Extentions;
using Common.Runtime.Session;
using Entities;
using Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EntityFrameworkCore.Audits
{
    public class AuditHelper : IAuditHelper
    {
        private readonly IUserSession _userSession;

        public AuditHelper(IUserSession userSession)
        {
            _userSession = userSession;
        }

        public void ApplyConcepts(EntityEntry entry)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    ApplyConceptsForAddedEntity(entry);
                    break;
                case EntityState.Modified:
                    ApplyConceptsForModifiedEntity(entry);
                    break;
                case EntityState.Deleted:
                    ApplyConceptsForDeletedEntity(entry);
                    break;
            }
        }

        private void ApplyConceptsForAddedEntity(EntityEntry entry)
        {
            SetCreationStatusForStatusProperty(entry);
            SetCreationAuditProperties(entry.Entity);
        }

        private void ApplyConceptsForModifiedEntity(EntityEntry entry)
        {
            SetModificationAuditProperties(entry.Entity);
        }

        private void ApplyConceptsForDeletedEntity(EntityEntry entry)
        {
            //CancelDeletionForSoftDelete(entry);
            SetModificationAuditProperties(entry.Entity);
        }

        private void CancelDeletionForSoftDelete(EntityEntry entry)
        {
            if (!(entry.Entity is IStatusableEntity))
            {
                return;
            }

            entry.Reload();
            entry.State = EntityState.Modified;
            entry.Entity.As<IStatusableEntity>().IsAlive = EntityStatus.Delete;

            SetModificationAuditProperties(entry.Entity);
        }

        private static void SetCreationStatusForStatusProperty(EntityEntry entry)
        {
            if (entry.Entity is IStatusableEntity)
            {
                entry.Entity.As<IStatusableEntity>().IsAlive = EntityStatus.Alive;
            }
        }

        private void SetModificationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is IUpdatedDateAuditableEntity)
            {
                entityAsObj.As<IUpdatedDateAuditableEntity>().UpdatedDate = DateTime.Now;
            }

            if (entityAsObj is IUpdatedUserAuditableEntity)
            {
                entityAsObj.As<IUpdatedUserAuditableEntity>().UpdatedUser = _userSession.UserName;
            }
        }

        private void SetCreationAuditProperties(object entityAsObj)
        {
            if (entityAsObj is ICreatedDateAuditableEntity)
            {
                entityAsObj.As<ICreatedDateAuditableEntity>().CreatedDate = DateTime.Now;
            }

            if (entityAsObj is ICreatedUserAuditableEntity)
            {
                entityAsObj.As<ICreatedUserAuditableEntity>().CreatedUser = _userSession.UserName;
            }
        }
    }
}