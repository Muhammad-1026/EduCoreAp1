using EduCoreApi.Domain.Common;
using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Group : Entity
{
    public string Name { get; private set; } = default!;
    public int CourseNumber { get; private set; }
    public string? Description { get; private set; }

    public Guid SpecialityId { get; private set; }
    public Speciality Speciality { get; private set; } = default!;

    public List<Student> Students { get; private set; } = new();
    public List<TimeTable> TimeTables { get; private set; } = new();

    public Group(string name, int courseNumber, Guid createdBy, string? description = null) : base(createdBy)
    {
        SetName(name);
        SetCourseNumber(courseNumber);
        SetDescription(description);
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(GroupErrors.NameCantBeNull);

        Name = name;
    }

    public void SetCourseNumber(int courseNumber)
    {
        if (courseNumber < 1 || courseNumber > 6)
            throw new BussinessLogicException(GroupErrors.CourseNumberIsInvalid);

        CourseNumber = courseNumber;
    }

    public void SetDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description) ? null : description;
    }

    public static class GroupErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Group.NameCantBeNull",
            "Название группы обязательно."
        );

        public static readonly Error CourseNumberIsInvalid = new(
            "Group.CourseNumberIsInvalid",
            "Номер курса должен быть от 1 до 6."
        );

    }
}