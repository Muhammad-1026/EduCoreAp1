using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Application.Feature.Speciality.Models;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Students.Models;

public class GetStudentDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public bool IsDormitoryResident { get; set; }
    public string? ImageUrl { get; set; }
    public Gender Gender { get; set; }
    public bool IsActive { get; set; }

    public GroupDto GroupDto { get; set; } = default!;
    public SpecialityDto SpecialityDto { get; set; } = default!;
}