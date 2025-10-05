using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Teachers.Models;

public class GetTeacherDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }
    public Guid DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
}