using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Specialty : Entity
{
    public string Name { get; private set; } = default!;
    public string Code { get; private set; } = default!;
    public string? Description { get; private set; }

    public Specialty(string name, string code)
    {
        Name = name;
        Code = code;
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
        if (string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(SpecialtyErrors.DescriptionCantBeNull);

        Description = description;
    }

    public class SpecialtyErrors
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