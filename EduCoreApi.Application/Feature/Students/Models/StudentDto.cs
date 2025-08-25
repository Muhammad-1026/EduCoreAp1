using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Groups.Models;

namespace EduCoreApi.Application.Feature.Students.Models;

public class StudentDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string? ImageUrl { get; set; }
    public string Gender { get; set; } = "Unknown";

    public required Guid GroupId { get; set; }
    public GroupDto Group { get; set; } = default!;

    public Guid CourseId { get; set; }
    public CourseDto Course { get; set; } = default!;
}