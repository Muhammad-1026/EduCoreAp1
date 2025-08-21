using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Students.Models;

public class GetStudentDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = default!;
    public DateTime BirthDate { get; set; }
    public string? Email { get; set; }
    public string PhoneNumber { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
    public Gender Gender { get; private set; } = Gender.Unknown;
    public Guid GroupId { get; set; }
}  
 