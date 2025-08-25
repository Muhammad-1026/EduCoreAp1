using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Courses;

public class CourseError
{
    public static readonly Error NotFound = new(
        "Course.NotFound",
        "Курс не найден"
    );
}
