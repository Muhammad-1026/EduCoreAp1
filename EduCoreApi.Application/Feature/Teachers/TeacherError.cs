using EduCoreApi.Shared.Models;

namespace EduCoreApi.Application.Feature.Teachers;

public class TeacherError
{
    public static readonly Error NotFound = new(
        "Teacher.NotFound",
        "Учитель не найден"
    );
}