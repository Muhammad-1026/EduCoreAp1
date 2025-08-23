using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Subject : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public Subject(string name, Guid createdBy, string? description = null) : base(createdBy)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(SubjectErrors.NameCantBeNull);

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description is not null && string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(SubjectErrors.DescriptionCantBeEmpty);

        Description = description;
    }

    public static class SubjectErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Subject.NameCantBeNull",
            "Название предмета обязательно."
        );

        public static readonly Error DescriptionCantBeEmpty = new(
           "Subject.DescriptionCantBeEmpty",
           "Описание не может быть пустым или состоять только из пробелов."
        );
    }
}
