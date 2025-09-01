namespace EduCoreApi.Application.Feature.Departments.Models;

public class UpdateDepartmentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
