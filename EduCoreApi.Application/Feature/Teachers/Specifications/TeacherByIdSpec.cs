using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;
using Ardalis.Specification;

namespace EduCoreApi.Application.Feature.Teachers.Specifications;

public sealed class TeacherByIdSpec : DbSpecifications<Teacher>
{
    public Guid TeacherId { get; }

    public TeacherByIdSpec(Guid teacherId, bool asNoTracking = false)
    {
        TeacherId = teacherId;

        Query
            .Where(t => t.Id == TeacherId)
            .Include(t => t.Department);

        if (asNoTracking)
            Query.AsNoTracking();
    }
}