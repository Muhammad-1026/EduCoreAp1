using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.Students.Specifications;

public sealed class StudentByIdSpec : DbSpecifications<Student>
{
    public Guid StudentId { get; }

    public StudentByIdSpec(Guid studentId, bool asNoTracking = false)
    {
        StudentId = studentId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(student => student.Id == StudentId);

        Query.Include(student => student.Group)
            .ThenInclude(group => group.Course)
            .ThenInclude(course => course.Department);
    }
}   