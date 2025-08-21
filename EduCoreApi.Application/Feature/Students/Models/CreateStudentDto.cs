using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Students.Models;

public class CreateStudentDto
{
    public string FullName { get; private set; } = default!;
    public DateTime BirthDate { get; private set; }
    public string? Email { get; private set; }
    public string PhoneNumber { get; private set; } = default!;
    public string Address { get; private set; } = default!;
    public bool IsDormitoryResident { get; private set; }
    public string? ImageUrl { get; private set; }
    public Gender Gender { get; private set; } = Gender.Unknown;
    public bool IsActive { get; private set; }

    public Guid GroupId { get; private set; }
    public Group Group { get; private set; } = default!;
    public ICollection<Grade> Grades { get; private set; } = new List<Grade>();
    public ICollection<Attendance> Attendances { get; private set; } = new List<Attendance>();
}
