using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.Subjects.Specifications;

public class SubjectByIdSpec : DbSpecifications<Subject>
{
    public Guid SubjectId { get; }

    public SubjectByIdSpec(Guid subjectId, bool asNoTracking = false)
    {
        SubjectId = subjectId;

        if (asNoTracking)
            Query.AsNoTracking();

        Query.Where(s => s.Id == SubjectId);
    }
}