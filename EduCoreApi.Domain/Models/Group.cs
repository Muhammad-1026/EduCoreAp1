using EduCoreApi.Shared.Exeptions;
using EduCoreApi.Shared.Models;

namespace EduCoreApi.Domain.Models;

public class Group
{
    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;

    public int CourseNumber { get; private set; }

    public string? Description { get; private set; }

    public Group(string name, int courseNumber)
    {
        Name = name;
        CourseNumber = courseNumber;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new BussinessLogicException(GroupErrors.NameCantBeNull);

        Name = name;
    }

    public void SetCourseNumber(int courseNumber)
    {
        if (courseNumber <= 0)
            throw new BussinessLogicException(GroupErrors.CourseNumberCantBeNull);

        CourseNumber = courseNumber;
    }

    public void SetDescription(string? description)
    {
        if (string.IsNullOrWhiteSpace(description))
            throw new BussinessLogicException(GroupErrors.DescriptionCantBeNull);
        Description = description;
    }

    public class GroupErrors
    {
        public static readonly Error NameCantBeNull = new(
            "Group.NameCantBeNull",
            "Name cannot be null or empty."
        );

        public static readonly Error CourseNumberCantBeNull = new(
            "Group.CourseNumberCantBeNull",
            "Course number cannot be null or empty."
        );

        public static readonly Error DescriptionCantBeNull = new(
            "Group.DescriptionCantBeNull",
            "Description cannot be null or empty."
        );
    }
}
