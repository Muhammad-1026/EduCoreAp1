namespace EduCoreApi.Application.Feature.Speciality.Models;

public class CreateSpecialityDto
{
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }

    public Guid DepartmentId { get; set; }
}