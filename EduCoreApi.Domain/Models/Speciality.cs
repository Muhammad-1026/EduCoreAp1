using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Speciality : Entity
{
    public string Name { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string? Description { get; private set; }

    public Guid DepartmentId { get; private set; }
    public Department Department { get; private set; } = default!;

    public List<Group> Groups { get; set; } = new();
    public List<Student> Students { get; set; } = new();

    public Speciality(string name, string code, Guid departmentId, Guid createdBy, string? description = null) : base(createdBy)
    {
        SetName(name);
        SetCode(code);
        SetDepartmentId(departmentId);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(SpecialtyErrors.NameCantBeNull);

        Name = name;
    }

    public void SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new BussinessLogicException(SpecialtyErrors.CodeCantBeNull);

        Code = code;
    }

    public void SetDescription(string? description)
    {
        if (description is not null && string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(SpecialtyErrors.DescriptionCantBeNull);

        Description = description;
    }

    public void SetDepartmentId(Guid departmentId)
    {
        if (departmentId == Guid.Empty)
            throw new BussinessLogicException(SpecialtyErrors.DepartmentIdCantBeNull);

        DepartmentId = departmentId;
    }

    public static class SpecialtyErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Specialty.NameCantBeNull",
            "Название специальности обязательно."
        );

        public static readonly Error CodeCantBeNull = new(
            "Specialty.CodeCantBeNull",
            "Код специальности обязателен."
        );

        public static readonly Error DescriptionCantBeNull = new(
            "Specialty.DescriptionCantBeNull",
            "Описание не может быть пустым или состоять только из пробелов."
        );

        public static readonly Error DepartmentIdCantBeNull = new(
            "Specialty.DepartmentIdCantBeNull",
            "Идентификатор кафедры обязателен."
        );
    }
}