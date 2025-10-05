using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Subjects.Models;

namespace EduCoreApi.Application.Feature.CourseSubjects.Models;

public class UpdateCourseSubjectDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public UpdateCourseDto UpdateCourseDto { get; set; } = default!;

    public Guid SubjectId { get; set; }
    public UpdateSubjectDto UpdateSubjectDto { get; set; } = default!;

    public int Credits { get; set; }
    public int Hours { get; set; }
}