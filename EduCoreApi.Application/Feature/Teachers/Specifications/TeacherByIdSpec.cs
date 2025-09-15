using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.Teachers.Specifications;

public sealed class TeacherByIdSpec : DbSpecifications<Teacher>
{
    public Guid TeacherId { get; }
    public TeacherByIdSpec(Guid teacherId, bool asNoTracking = false)
    {
        TeacherId = teacherId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(teacher => teacher.Id == TeacherId);
        Query.Include(teacher => teacher.Department);
    }
}
