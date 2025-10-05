using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Teachers.Models;

namespace EduCoreApi.Application.Feature.CourseTeachers.Models;

public class UpdateCourseTeacherDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public UpdateCourseDto UpdateCourseDto { get; set; } = default!;

    public Guid TeacherId { get; set; }
    public UpdateTeacherDto UpdateTeacherDto { get; set; } = default!;

    public bool IsCurator { get; set; }
}