namespace EduCoreApi.Application.Feature.Specialities.Models;

public class UpdateSpecialityDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Code { get; set; }
    public string? Description { get; set; }

    public Guid DepartmentId { get; set; }
}