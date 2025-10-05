using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Subjects.Models;

namespace EduCoreApi.Application.Feature.CourseSubjects.Models;

public class CreateCourseSubjectDto
{
    public Guid CourseId { get; set; }
    public CreateCourseDto CreateCourseDto { get; set; } = default!;

    public Guid SubjectId { get; set; }
    public CreateSubjectDto CreateSubjectDto { get; set; } = default!;

    public int Credits { get; set; }
    public int Hours { get; set; }
}