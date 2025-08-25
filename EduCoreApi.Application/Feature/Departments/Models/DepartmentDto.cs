namespace EduCoreApi.Application.Feature.Departments.Models;

public class DepartmentDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}
