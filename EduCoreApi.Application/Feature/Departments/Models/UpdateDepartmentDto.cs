namespace EduCoreApi.Application.Feature.Departments.Models;

public class UpdateDepartmentDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
