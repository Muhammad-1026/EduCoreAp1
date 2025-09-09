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

        //TODO
        CreatedBy = Guid.Parse("36aae2fe-248a-40b4-8523-01c5fa0e0bf5");
    }

    public void UpdateAudit(Guid updatedBy)
    {
        UpdatedDate = DateTime.UtcNow;

        //TODO
        UpdatedBy = Guid.Parse("9cb48cb8-57af-4f0f-ba23-b70d9aada696"); ;
    }
}