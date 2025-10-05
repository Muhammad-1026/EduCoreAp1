using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Teachers.Models;

namespace EduCoreApi.Application.Feature.CourseTeachers.Models;

public class GetCourseTeacherDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public GetCourseDto GetCourseDto { get; set; } = default!;

    public Guid TeacherId { get; set; }
    public GetTeacherDto GetTeacherDto { get; set; } = default!;

    public bool IsCurator { get; set; }
}