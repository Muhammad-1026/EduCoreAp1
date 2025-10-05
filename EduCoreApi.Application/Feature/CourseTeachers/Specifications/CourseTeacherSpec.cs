using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.CourseTeachers.Specifications;

public class CourseTeacherSpec : DbSpecifications<CourseTeacher>
{
    public Guid CourseTeacherId { get; }

    public CourseTeacherSpec(Guid courseTeacherId, bool asNoTracking = false)
    {
        CourseTeacherId = courseTeacherId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(d => d.Id == CourseTeacherId)
            .Include(d => d.Course)
            .Include(d => d.Teacher);
    }
}