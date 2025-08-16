using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Subject : Entity
{
    public string Name { get; private set; } = default!;

    public string? Description { get; private set; }

    public Subject(string name)
    {
        Name = name;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(SubjectErrors.NameCantBeNull);

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(SubjectErrors.DescriptionCantBeNull);

        Description = description;
    }

    public class SubjectErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Subject.NameCantBeNull",
            "Name cannot be null or empty."
        );

        public static readonly Error DescriptionCantBeNull = new(
            "Subject.DescriptionCantBeNull",
            "Description cannot be null or empty."
        );
    }
}