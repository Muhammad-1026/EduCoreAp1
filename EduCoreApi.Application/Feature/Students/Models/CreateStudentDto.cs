using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Students.Models;

public class CreateStudentDto
{
    public string FullName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string Address { get; set; } = default!;
    public bool IsDormitoryResident { get; set; }
    public string? ImageUrl { get; set; }
    public Gender Gender { get; set; } = Gender.Unknown;

    public Guid GroupId { get; set; }
}
