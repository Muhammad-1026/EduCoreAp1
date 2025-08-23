namespace EduCoreApi.Application.Feature.Groups.Models;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int CourseNumber { get; set; }
    public string? Description { get; set; }
}
