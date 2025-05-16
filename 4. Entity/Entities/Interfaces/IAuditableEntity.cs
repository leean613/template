namespace Entities.Interfaces
{
    public interface IAuditableEntity : ICreatedAuditableEntity, IUpdatedAuditableEntity
    {
    }
}