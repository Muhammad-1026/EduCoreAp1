using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.TimeTables.Models;

namespace EduCoreApi.Application.Feature.Groups.Models;

public class GetGroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public int CourseNumber { get; set; }
    public string? Description { get; set; }

    public Guid SpecialityId { get; set; }
    public string SpecialityName { get; set; } = default!;

    public List<GetStudentDto> Students { get; set; } = new();
    public List<GetTimeTableDto> TimeTables { get; set; } = new();
}
