using EduCoreApi.Application.Feature.Courses.Models;
using EduCoreApi.Application.Feature.Subjects.Models;

namespace EduCoreApi.Application.Feature.CourseSubjects.Models;

public class GetCourseSubjectDto
{
    public Guid Id { get; set; }
    public Guid CourseId { get; set; }
    public GetCourseDto GetCourseDto { get; set; } = default!;
    public string CourseName { get; set; } = null!;

    public Guid SubjectId { get; set; }
    public GetSubjectDto GetSubjectDto { get; set; } = default!;
    public string SubjectName { get; set; } = null!;

    public int Credits { get; set; }
    public int Hours { get; set; }
}