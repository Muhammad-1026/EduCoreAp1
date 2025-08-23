using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Department : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public Department(string name, Guid createdBy, string? description = null) : base(createdBy)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(DepartmentErrors.NameCantBeNull);

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description is not null && string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(DepartmentErrors.DescriptionCantBeEmpty);

        Description = description;
    }

    public static class DepartmentErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Department.NameCantBeNull",
            "Название кафедры обязательно."
        );

        public static readonly Error DescriptionCantBeEmpty = new(
            "Department.DescriptionCantBeEmpty",
            "Описание не может быть пустым или состоять только из пробелов."
        );
    }
}