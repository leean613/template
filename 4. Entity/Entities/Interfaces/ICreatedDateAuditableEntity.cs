using System;

namespace Entities.Interfaces
{
    public interface ICreatedDateAuditableEntity
    {
        DateTime? CreatedDate { get; set; }
    }
}