namespace EduCoreApi.Domain.Common;

public class Entity
{
    public long Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public DateTime? UpdatedDate { get; set; }

    public long CreatedBy { get; set; }

    public long? UpdatedBy { get; set; }
}