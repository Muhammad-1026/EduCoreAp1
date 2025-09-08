using Ardalis.Specification;
using EduCoreApi.Application.Common.Specifications;
using EduCoreApi.Domain.Models;

namespace EduCoreApi.Application.Feature.Departments.Specifications;

public class DepartmentByIdSpecification : DbSpecifications<Department>
{
    public Guid DepartmentId { get; }

    public DepartmentByIdSpecification(Guid departmentId, bool asNoTracking = false)
    {
        DepartmentId = departmentId;

        if (asNoTracking)
            Query.AsNoTracking();

       Query.Where(d => d.Id == DepartmentId);
    }
}
