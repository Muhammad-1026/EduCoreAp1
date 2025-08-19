using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Course : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public ICollection<CourseTeacher> Teachers { get; private set; } = new List<CourseTeacher>();
    public ICollection<CourseSubject> Subjects { get; private set; } = new List<CourseSubject>();
    public ICollection<CourseStudent> Students { get; private set; } = new List<CourseStudent>();

    public Course(string name, string? description, Guid createdBy) : base(createdBy)
    {
        SetName(name);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(CourseErrors.NameCantBeNull);

        Name = name;
    }

    public void SetDescription(string? description)
    {
        if (description is not null && string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(CourseErrors.DescriptionCantBeEmpty);

        Description = description;
    }

    public static class CourseErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Course.NameCantBeNull",
            "Название курса не может быть пустым."
        );

        public static readonly Error DescriptionCantBeEmpty = new(
            "Course.DescriptionCantBeEmpty",
            "Описание не может быть пустым или состоять только из пробелов."
        );
    }
}
