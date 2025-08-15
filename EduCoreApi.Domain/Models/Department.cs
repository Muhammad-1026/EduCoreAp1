using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Department : Entity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public Department(string name)
    {
        Name = name;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(DepartmentErrors.NameCantBeNull);

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(DepartmentErrors.DescriptionCantBeNull);

        Description = description;
    }

    public class DepartmentErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Department.NameCantBeNull",
            "Name cannot be null or empty."
        );
        public static readonly Error DescriptionCantBeNull = new(
            "Department.DescriptionCantBeNull",
            "Description cannot be null or empty."
        );
    }
}