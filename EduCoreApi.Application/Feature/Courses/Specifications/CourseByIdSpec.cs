using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.Courses.Specifications;

public class CourseByIdSpec : DbSpecifications<Course>
{
    public Guid CourseId { get; }

    public CourseByIdSpec(Guid courseId, bool asNoTracking = false)
    {
        CourseId = courseId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(x => x.Id == CourseId);
    }
}