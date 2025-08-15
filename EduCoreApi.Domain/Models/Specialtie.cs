using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Specialtyie : Entity
{
    public string Name { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string? Description { get; private set; }

    public Specialtyie(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(SpecialtyieErrors.NameCantBeNull);

        Name = name;
    }

    public void SetCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new BussinessLogicException(SpecialtyieErrors.CodeCantBeNull);

        Code = code;
    }

    public void SetDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(SpecialtyieErrors.DescriptionCantBeNull);

        Description = description;
    }

    public class SpecialtyieErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Specialty.NameCantBeNull",
            "Name cannot be null or empty."
        );

        public static readonly Error CodeCantBeNull = new(
            "Specialty.CodeCantBeNull",
            "Code cannot be null or empty."
        );

        public static readonly Error DescriptionCantBeNull = new(
            "Specialty.DescriptionCantBeNull",
            "Description cannot be null or empty."
        );
    }
}