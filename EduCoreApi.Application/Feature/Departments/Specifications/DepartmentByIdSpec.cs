using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;
using Ardalis.Specification;

namespace EduCoreApi.Application.Feature.Departments.Specifications;

public class DepartmentByIdSpec : DbSpecifications<Department>
{
    public Guid DepartmentId { get; }

    public DepartmentByIdSpec(Guid departmentId, bool asNoTracking = false)
    {
        DepartmentId = departmentId;

        if (asNoTracking)
            Query.AsNoTracking();

       Query.Where(d => d.Id == DepartmentId);
    }
}
