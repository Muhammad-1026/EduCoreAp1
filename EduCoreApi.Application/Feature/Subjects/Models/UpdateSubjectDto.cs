namespace EduCoreApi.Application.Feature.Subjects.Models;

public class UpdateSubjectDto
{
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
}
