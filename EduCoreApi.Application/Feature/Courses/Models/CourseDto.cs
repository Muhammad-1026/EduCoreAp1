using EduCoreApi.Application.Feature.Departments.Models;

namespace EduCoreApi.Application.Feature.Courses.Models;

public class CourseDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get;  set; }

    public Guid DepartmentId { get; set; }
    public required DepartmentDto Department { get; set; }
}