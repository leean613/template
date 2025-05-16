namespace Entities.Interfaces
{
    public interface ICreatedAuditableEntity : ICreatedDateAuditableEntity, ICreatedUserAuditableEntity
    {
    }
}