using System;

namespace Entities.Interfaces
{
    public interface IUpdatedDateAuditableEntity
    {
        DateTime? UpdatedDate { get; set; }
    }
}