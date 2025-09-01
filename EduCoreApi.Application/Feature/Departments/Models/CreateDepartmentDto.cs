namespace EduCoreApi.Application.Feature.Departments.Models;

public class CreateDepartmentDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
