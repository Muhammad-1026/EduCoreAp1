namespace EduCoreApi.Application.Feature.Specialities.Models;

public class CreateSpecialityDto
{
    public  string Name { get; set; } = default!;
    public  string Code { get; set; } = default!;
    public string? Description { get; set; }

    public required Guid DepartmentId { get; set; }
}