namespace EduCoreApi.Application.Feature.Groups.Models;

public class GetGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int CourseNumber { get; set; }
    public string? Description { get; set; }

    public Guid SpecialityId { get; set; }
    public string SpecialityName { get; set; } = default!;
}
