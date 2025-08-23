using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Domain.Models;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Students.Models;

public class GetStudentDto
{
    public Guid Id { get; set; }
    public required string FullName { get; set; }
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public required string PhoneNumber { get; set; }
    public string? ImageUrl { get; set; }
    public Gender Gender { get; set; } = Gender.Unknown;
    public required GroupDto Group { get; set; }
    public required Specialty Specialty { get; set; }
}
