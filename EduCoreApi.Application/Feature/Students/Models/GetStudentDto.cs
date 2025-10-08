using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Application.Feature.Specialities.Models;
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
    public required string GroupName { get; set; }
    public required string SpecialityName { get; set; }  

    public GetGroupDto GetGroupDto { get; set; } = default!;
    public GetSpecialityDto GetSpecialityDto { get; set; } = default!;
}