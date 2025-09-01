namespace EduCoreApi.Application.Feature.Courses.Models;

public class CreateCourseDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
