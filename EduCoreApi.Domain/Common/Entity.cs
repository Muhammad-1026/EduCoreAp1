namespace EduCoreApi.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime? UpdatedDate { get; private set; }

    public Guid CreatedBy { get; private set; }

    public Guid? UpdatedBy { get; private set; }

    protected Entity(Guid createdBy)
    {
        Id = Guid.NewGuid();
        CreatedDate = DateTime.UtcNow;
        CreatedBy = createdBy;
    }

    public void UpdateAudit(Guid updatedBy)
    {
        UpdatedDate = DateTime.UtcNow;
        UpdatedBy = updatedBy;
    }
}