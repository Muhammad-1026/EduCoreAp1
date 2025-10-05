using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.CourseSubjects.Specifications;

public class CourseSubjectByIdSpec : DbSpecifications<CourseSubject>
{
    public Guid CourseSubjectById { get; }

    public CourseSubjectByIdSpec(Guid courseSubjectId, bool asNoTracking = false)
    {
        CourseSubjectById = courseSubjectId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(d => d.Id == CourseSubjectById)
            .Include(d => d.Course)
            .Include(d => d.Subject);
    }
}