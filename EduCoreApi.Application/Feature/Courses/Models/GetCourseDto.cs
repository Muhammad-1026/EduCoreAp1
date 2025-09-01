namespace EduCoreApi.Application.Feature.Courses.Models;

public class GetCourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
