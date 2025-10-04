namespace EduCoreApi.Application.Feature.Groups.Models;

public class CreateGroupDto
{
    public required string Name { get; set; }
    public int CourseNumber { get; set; }
    public string? Description { get; set; }

    public required Guid SpecialityId { get; set; }
}