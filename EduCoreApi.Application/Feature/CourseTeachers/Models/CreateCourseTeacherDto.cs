using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Teachers.Models;

namespace EduCoreApi.Application.Feature.CourseTeachers.Models;

internal class CreateCourseTeacherDto
{
    public Guid CourseId { get; set; }
    public CreateCourseDto CreateCourseDto { get; set; } = default!;

    public Guid TeacherId { get; set; }
    public CreateTeacherDto CreateTeacherDto { get; set; } = default!;

    public bool IsCurator { get; set; }
}