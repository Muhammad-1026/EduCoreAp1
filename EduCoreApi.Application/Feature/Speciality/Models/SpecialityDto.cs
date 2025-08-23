namespace EduCoreApi.Application.Feature.Speciality.Models;

public class SpecialityDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
    public string? Description { get; set; }
    public Guid DepartmentId { get; set; }
}
