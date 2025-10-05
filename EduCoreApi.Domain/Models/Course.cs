using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Course : Entity
{
    public string Name { get; private set; } = default!;
    public string? Description { get; private set; }

    public List<CourseTeacher> CourseTeachers { get; private set; } = new();
    public List<CourseSubject> CourseSubjects { get; private set; } = new();

    public Course(string name, Guid createdBy) : base(createdBy)
    {
        SetName(name);
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